using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using System;

namespace SIS.WebServer.Routing
{
    public class Route
    {
        public Route(HttpRequestMethod method, string path, Func<IHttpRequest, IHttpResponse> action)
        {
            Method = method;
            Path = path;
            Action = action;
        }

        public HttpRequestMethod Method { get; set; }

        public string Path { get; set; }

        public Func<IHttpRequest, IHttpResponse> Action { get; set; }

        public bool HasEqual(HttpRequestMethod otherMethod, string otherPath)
        {
            return otherMethod == this.Method && string.Compare(otherPath, this.Path, true) == 0;
        }
    }
}
