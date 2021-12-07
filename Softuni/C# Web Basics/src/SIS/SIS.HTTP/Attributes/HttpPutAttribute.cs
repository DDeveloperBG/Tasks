using SIS.HTTP.Enums;

namespace SIS.HTTP.Attributes
{
    public class HttpPutAttribute : HttpRequestMethodAttribute
    {
        public HttpPutAttribute()
        {
        }

        public HttpPutAttribute(string url) : base(url)
        {
        }

        public override HttpRequestMethod RequestMethod => HttpRequestMethod.PUT;
    }
}
