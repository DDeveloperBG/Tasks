using SIS.HTTP.Common;
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using System.Linq;
using System.Text;

namespace SIS.HTTP.Responses
{
    public class HttpResponse : IHttpResponse
    {
        private byte[] content;

        public HttpResponse()
        {
            Headers = new HttpHeaderCollection();
            Cookies = new HttpCookieCollection();
            Content = new byte[0];
        }

        public HttpResponse(HttpResponseStatusCode responseStatusCode)
            : this()
        {
            StatusCode = responseStatusCode;
        }

        public HttpResponseStatusCode StatusCode { get; set; }

        public IHttpHeaderCollection Headers { get; }

        public IHttpCookieCollection Cookies { get; }

        public byte[] Content
        {
            get => content;
            set
            {
                content = value;

                if (!Headers.ContainsHeader(HttpHeader.ContentLength))
                {
                    Headers.AddHeader(
                        new HttpHeader(HttpHeader.ContentLength, Content.Length.ToString()));
                }
                else
                {
                    Headers
                        .GetHeader(HttpHeader.ContentLength)
                        .Value = Content.Length.ToString();
                }               
            }
        }

        public void AddHeader(HttpHeader header)
        {
            header.ThrowIfNull(nameof(header));

            Headers.AddHeader(header);
        }

        public void AddCookie(HttpCookie cookie)
        {
            cookie.ThrowIfNull(nameof(cookie));

            Cookies.AddCookie(cookie);
        }

        public byte[] GetBytes()
        {
            byte[] mainPart = Encoding.UTF8.GetBytes(this.ToString());

            return mainPart.Concat(Content).ToArray();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result
                .Append($"{GlobalConstants.HttpOneProtocolFragment} {(int)StatusCode} {StatusCode}")
                .Append(GlobalConstants.HttpNewLine)
                .Append(Headers)
                .Append(GlobalConstants.HttpNewLine);

            if (Cookies.HasCookies())
            {
                result
                    .Append(Cookies)
                    .Append(GlobalConstants.HttpNewLine);
            }

            return result.ToString();
        }
    }
}
