using SIS.HTTP.Common;
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.HTTP.Sessions;
using SIS.WebServer.Results;
using SIS.WebServer.Routing;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SIS.WebServer
{
    public class ConnectionHandler
    {
        private const int BufferSize = 4096;

        private readonly Socket client;

        private readonly IServerRoutingTable serverRoutingTable;

        public ConnectionHandler(
            Socket client,
            IServerRoutingTable serverRoutingTable)
        {
            client.ThrowIfNull(nameof(client));
            serverRoutingTable.ThrowIfNull(nameof(serverRoutingTable));

            this.client = client;
            this.serverRoutingTable = serverRoutingTable;
        }

        public async Task ProcessRequestAsync()
        {
            try
            {
                var httpRequest = await ReadRequestAsync();

                if (httpRequest != null)
                {
                    Console.WriteLine($"Processing: {httpRequest.RequestMethod} {httpRequest.Path}...");

                    var sessionId = SetRequestSession(httpRequest);
                    var httpResponse = HandleRequest(httpRequest);

                    SetResponseSession(httpResponse, sessionId);

                    await PrepareResponseAsync(httpResponse);
                }
            }
            catch (BadRequestException e)
            {
                await PrepareResponseAsync(
                     new TextResult(e.ToString(),
                     HttpResponseStatusCode.BadRequest));
            }
            catch (Exception e)
            {
                await PrepareResponseAsync(
                     new TextResult(e.ToString(),
                     HttpResponseStatusCode.InternalServerError));
            }

            client.Shutdown(SocketShutdown.Both);
        }

        private string SetRequestSession(IHttpRequest httpRequest)
        {
            string sessionId;

            if (httpRequest.Cookies.ContainsCookie(HttpSessionStorage.SessionCookieKey))
            {
                var cookie = httpRequest.Cookies.GetCookie(HttpSessionStorage.SessionCookieKey);
                sessionId = cookie.Value;
                httpRequest.Session = HttpSessionStorage.GetSession(sessionId);
            }
            else
            {
                sessionId = Guid.NewGuid().ToString();
                httpRequest.Session = HttpSessionStorage.GetSession(sessionId);
            }

            return sessionId;
        }

        private void SetResponseSession(IHttpResponse httpResponse, string sessionId)
        {
            if (sessionId != null)
            {
                httpResponse
                    .AddCookie(new HttpCookie(HttpSessionStorage.SessionCookieKey, sessionId));
            }
        }

        private async Task<IHttpRequest> ReadRequestAsync()
        {
            var result = new StringBuilder();
            var data = new ArraySegment<byte>(new byte[BufferSize]);

            while (true)
            {
                int numberOfBytesRead = await client.ReceiveAsync(data.Array, SocketFlags.None);

                if (numberOfBytesRead == 0)
                {
                    break;
                }

                var bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesRead);
                result.Append(bytesAsString);

                if (numberOfBytesRead < BufferSize)
                {
                    break;
                }
            }

            if (result.Length == 0)
            {
                return null;
            }

            return new HttpRequest(result.ToString());
        }

        private string GetNotSupportedRouteMessage(HttpRequestMethod requestMethod, string path)
        {
            return $"Route with method {requestMethod} and path \"{path}\" not found.";
        }

        private IHttpResponse HandleRequest(IHttpRequest httpRequest)
        {
            if (!serverRoutingTable
                    .Contains(httpRequest.RequestMethod, httpRequest.Path))
            {
                return new TextResult(
                    GetNotSupportedRouteMessage(httpRequest.RequestMethod, httpRequest.Path),
                    HttpResponseStatusCode.NotFound);
            }

            return serverRoutingTable
                .Get(httpRequest.RequestMethod, httpRequest.Path)
                .Invoke(httpRequest);
        }

        private async Task PrepareResponseAsync(IHttpResponse httpResponse)
        {
            byte[] byteSegments = httpResponse.GetBytes();

            await client.SendAsync(byteSegments, SocketFlags.None);
        }
    }
}
