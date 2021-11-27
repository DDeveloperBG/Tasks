using SIS.HTTP.Common;
using System;
using System.Text;

namespace SIS.HTTP.Cookies
{
    public class HttpCookie
    {
        private const int HttpCookieDefaultExpirationDays = 3;
        private const string HttpCookieDefaultPath = "/";

        public HttpCookie(
                string key,
                string value,
                int expires = HttpCookieDefaultExpirationDays,
                string path = HttpCookieDefaultPath
            )
        {
            key.ThrowIfNullOrEmpty(nameof(key));
            value.ThrowIfNullOrEmpty(nameof(value));

            Key = key;
            Value = value;
            Path = path;

            IsNew = true;
            Expires = DateTime.UtcNow.AddDays(expires);
        }

        public HttpCookie(
                string key,
                string value,
                bool isNew,
                int expires = HttpCookieDefaultExpirationDays,
                string path = HttpCookieDefaultPath
            )
            : this(key, value, expires, path)
        {
            IsNew = isNew;
        }

        public string Key { get; }
        public string Value { get; }

        public DateTime Expires { get; private set; }

        public string Path { get; set; }

        public bool IsNew { get; }

        public bool HttpOnly { get; set; } = true;

        public void Delete()
        {
            Expires = DateTime.UtcNow.AddDays(-1);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append($"{Key}={Value}; ");
            result.Append($"Expires={Expires:R}");

            if (HttpOnly)
            {
                result.Append($"; HttpOnly");
            }

            result.Append($"; Path={Path}");

            return result.ToString();
        }
    }
}
