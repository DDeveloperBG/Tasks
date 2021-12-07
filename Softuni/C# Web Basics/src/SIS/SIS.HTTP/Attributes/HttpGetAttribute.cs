using SIS.HTTP.Enums;

namespace SIS.HTTP.Attributes
{
    public class HttpGetAttribute : HttpRequestMethodAttribute
    {
        public HttpGetAttribute()
        {
        }

        public HttpGetAttribute(string url) : base(url) 
        {
        }

        public override HttpRequestMethod RequestMethod => HttpRequestMethod.GET;
    }
}