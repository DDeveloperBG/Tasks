using SIS.HTTP.Common;
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Headers;
using SIS.HTTP.Sessions;
using System;
using System.Collections.Generic;
using System.Web;

namespace SIS.HTTP.Requests
{
    public class HttpRequest : IHttpRequest
    {
        private const char DefaultWordsSeparator = ' ';

        private const char HttpUrlQuerySeparator = '?';

        private const string HttpHeaderNameValueSeparator = ": ";

        private const string HttpCookieStringSeparator = "; ";

        private const char HttpParameterNameValueSeparator = '=';

        private const char HttpParameterSeparator = '&';

        public HttpRequest(string requetString)
        {
            requetString.ThrowIfNullOrEmpty(nameof(requetString));

            FormData = new Dictionary<string, object>();
            QueryData = new Dictionary<string, object>();
            Headers = new HttpHeaderCollection();
            Cookies = new HttpCookieCollection();

            ParseRequest(requetString);
        }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public IHttpCookieCollection Cookies { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        public IHttpSession Session { get; set; }

        private bool IsValidRequestLine(string[] requestLine)
        {
            return requestLine.Length == 3 && requestLine[2] == GlobalConstants.HttpOneProtocolFragment;
        }

        private bool IsValidRequestQueryString(string queryString, string[] queryParameters)
        {
            return !string.IsNullOrEmpty(queryString) && queryParameters.Length > 0;
        }

        private void ParseRequestMethod(string[] requestLine)
        {
            RequestMethod = Enum.Parse<HttpRequestMethod>(requestLine[0]);
        }

        private void ParseRequestUrl(string[] requestLine)
        {
            if (string.IsNullOrEmpty(requestLine[1]))
            {
                throw new BadRequestException();
            }

            Url = HttpUtility.UrlDecode(requestLine[1]);
        }

        private void ParseRequestPath()
        {
            int pathEndIndex = Url.IndexOf(HttpUrlQuerySeparator);

            if (pathEndIndex == -1)
            {
                pathEndIndex = Url.Length;
            }

            Path = Url.Substring(0, pathEndIndex);
        }

        private void ParseHeaders(string[] rawHeaders)
        {
            foreach (var rawHeader in rawHeaders)
            {
                string[] parts = rawHeader.Split(HttpHeaderNameValueSeparator, 2);

                if (parts[0] == HttpHeader.Cookie)
                {
                    ParseCookies(parts[1]);
                    continue;
                }

                if (parts.Length == 1 && parts[0] == string.Empty)
                {
                    break;
                }

                Headers.AddHeader(new HttpHeader(parts[0], parts[1]));
            }

            if (!Headers.ContainsHeader(HttpHeader.Host))
            {
                throw new BadRequestException();
            }
        }

        private void ParseCookies(string values)
        {
            values.ThrowIfNullOrEmpty(nameof(values));

            string[] cookies = values.Split(HttpCookieStringSeparator);

            foreach (var cookie in cookies)
            {
                string[] parts = cookie.Split(HttpParameterNameValueSeparator, 2);

                Cookies.AddCookie(new HttpCookie(parts[0], parts[1]));
            }
        }

        private void ParseQueryParameters()
        {
            if (!Url.Contains(HttpUrlQuerySeparator))
            {
                return;
            }

            int queryStartIndex = Url.IndexOf(HttpUrlQuerySeparator) + 1;
            string query = Url[queryStartIndex..];

            string[] queryParameters = query.Split(HttpParameterSeparator);

            if (!IsValidRequestQueryString(query, queryParameters))
            {
                throw new BadRequestException();
            }

            foreach (var parameter in queryParameters)
            {
                string[] parts = parameter.Split(HttpParameterNameValueSeparator, 2);

                QueryData.Add(parts[0], parts[1]);
            }
        }

        private void ParseFormDataParameters(string formData)
        {
            if (string.IsNullOrEmpty(formData))
            {
                return;
            }

            string[] parameters = formData.Split(HttpParameterSeparator);

            foreach (var param in parameters)
            {
                string[] parts = param.Split(HttpParameterNameValueSeparator, 2);

                FormData.Add(parts[0], HttpUtility.UrlDecode(parts[1]));
            }
        }

        private void ParseRequestParameters(string formData)
        {
            ParseQueryParameters();

            ParseFormDataParameters(formData);
        }

        private void ParseRequest(string requestString)
        {
            string[] splitRequestContent = requestString
                .Split(GlobalConstants.HttpNewLine);

            string[] requestLine = splitRequestContent[0]
                .Trim()
                .Split(DefaultWordsSeparator, StringSplitOptions.RemoveEmptyEntries);

            if (!IsValidRequestLine(requestLine))
            {
                throw new BadRequestException();
            }

            ParseRequestMethod(requestLine);
            ParseRequestUrl(requestLine);
            ParseRequestPath();

            ParseHeaders(splitRequestContent[1..]);

            ParseRequestParameters(splitRequestContent[^1]);
        }
    }
}
