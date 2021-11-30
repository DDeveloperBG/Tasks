using SIS.HTTP.Common;
using SIS.HTTP.Exceptions;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Cookies
{
    public class HttpCookieCollection : IHttpCookieCollection
    {
        private readonly Dictionary<string, HttpCookie> cookies;

        public HttpCookieCollection()
        {
            cookies = new Dictionary<string, HttpCookie>();
        }

        public void AddCookie(HttpCookie cookie)
        {
            cookies.ThrowIfNull(nameof(cookies));

            if (ContainsCookie(cookie.Key))
            {
                throw new InternalServerErrorException();
            }

            cookies.Add(cookie.Key, cookie);
        }

        public bool ContainsCookie(string key)
        {
            key.ThrowIfNullOrEmpty(nameof(key));

            return cookies.ContainsKey(key);
        }

        public HttpCookie GetCookie(string key)
        {
            key.ThrowIfNullOrEmpty(nameof(key));

            return cookies[key];
        }

        public bool HasCookies()
        {
            return cookies.Count > 0;
        }

        public IEnumerator<HttpCookie> GetEnumerator()
        {
            return cookies.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            foreach (var cookie in cookies.Values)
            {
                result
                    .Append($"{Headers.HttpHeader.SetCookie}: {cookie}")
                    .Append(GlobalConstants.HttpNewLine);
            }

            return result.ToString();
        }
    }
}
