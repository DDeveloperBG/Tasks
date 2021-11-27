using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Sessions;
using System.Collections.Generic;

namespace SIS.HTTP.Requests
{
    public interface IHttpRequest
    {
        public string Path { get; }
        public string Url { get; }

        public Dictionary<string, object> FormData { get; }
        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }
        public IHttpCookieCollection Cookies { get; }
        public HttpRequestMethod RequestMethod { get; }
        public IHttpSession Session { get; set; }
    }
}
