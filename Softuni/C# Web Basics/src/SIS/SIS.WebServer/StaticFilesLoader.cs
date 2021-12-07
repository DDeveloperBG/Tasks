using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;
using SIS.WebServer.Routing;
using System;
using System.Collections.Generic;
using System.IO;

namespace SIS.WebServer
{
    public static class StaticFilesLoader
    {
        private const string globalResoursesFolderName = "wwwroot";
        private const int StaticResourseDefaultExpirationDays = 3;

        public static IServerRoutingTable LoadStaticFiles(this IServerRoutingTable serverRoutingTable)
        {
            if (Directory.Exists(globalResoursesFolderName))
            {
                var filePaths = Directory.GetFiles(globalResoursesFolderName, "*", SearchOption.AllDirectories);

                foreach (var filePath in filePaths)
                {
                    string fileName = filePath.Replace(globalResoursesFolderName, string.Empty)
                        .Replace('\\', '/');

                    serverRoutingTable.Add(HttpRequestMethod.GET, fileName, CasheFile(filePath));
                }
            }

            return serverRoutingTable;
        }

        private static readonly Dictionary<string, string> contentTypes = new Dictionary<string, string>()
        {
            { ".css", "text/css" },
            { ".js", "text/javascript" },
            { ".html", "text/html" },
            { ".ico", "image/vnd.microsoft.icon" },
            { ".jpeg", "image/jpeg"},
            { ".jpg", "image/jpeg"},
            { ".png", "image/png" },
            { ".pdf", "application/pdf" }
        };

        private static Func<IHttpRequest, IHttpResponse> CasheFile(string path)
        {
            byte[] fileData = File.ReadAllBytes(path);
            string fileType = ExtractFileType(path);

            return (request) =>
            {
                ByteResult result =
                    new ByteResult(fileData, HttpResponseStatusCode.Ok, contentTypes[fileType]);
               
                result.Headers
                    .AddHeader(new HttpHeader(
                            HttpHeader.Cash,
                            $"public, max-age={new TimeSpan(StaticResourseDefaultExpirationDays, 0, 0).TotalSeconds}")
                    );

                return result;
            };
        }

        private static string ExtractFileType(string path)
        {
            int startIndex = path.LastIndexOf('.');

            return path[startIndex..];
        }
    }
}
