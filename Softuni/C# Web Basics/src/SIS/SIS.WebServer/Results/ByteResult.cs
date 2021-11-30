using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Responses;

namespace SIS.WebServer.Results
{
    public class ByteResult : HttpResponse
    {
        public ByteResult(
                byte[] content, 
                HttpResponseStatusCode responseStatusCode,
                string contentType)
            : base(responseStatusCode)
        {
            this.Headers.AddHeader(
                new HttpHeader(HttpHeader.ContentType, contentType));

            this.Content = content;
        }
    }
}
