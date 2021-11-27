using System.Collections.Generic;

namespace SIS.HTTP.Cookies
{
    public interface IHttpCookieCollection : IEnumerable<HttpCookie>
    {
        public void AddCookie(HttpCookie cookie);

        public bool ContainsCookie(string key);

        public HttpCookie GetCookie(string key);

        public bool HasCookies();
    }
}
