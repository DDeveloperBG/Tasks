using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;

namespace SIS.HTTP.Responses
{
    public interface IHttpResponse
    {
        public HttpResponseStatusCode StatusCode { get; set; }

        public byte[] Content { get; set; }

        public IHttpHeaderCollection Headers { get; }

        public IHttpCookieCollection Cookies { get; }

        public void AddHeader(HttpHeader header);

        public void AddCookie(HttpCookie cookie);

        public byte[] GetBytes();
    }
}
