using SIS.HTTP.Common;

namespace SIS.HTTP.Headers
{
    public class HttpHeader
    {
        public const string Host = "Host";
        public const string ContentType = "ContentType";
        public const string ContentLength = "Content-Length";
        public const string Location = "Location";
        public const string SetCookie = "Set-Cookie";
        public const string Cookie = "Cookie";

        public HttpHeader(string key, string value)
        {
            key.ThrowIfNullOrEmpty(nameof(key));
            value.ThrowIfNullOrEmpty(nameof(value));

            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Key}: {Value}";
        }
    }
}
