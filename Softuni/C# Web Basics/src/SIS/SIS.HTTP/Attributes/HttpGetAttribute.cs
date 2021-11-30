using SIS.HTTP.Enums;

namespace SIS.HTTP.Attributes
{
    public class HttpGetAttribute : HttpRequestMethodAttribute
    {
        public HttpGetAttribute()
        {
            RequestMethod = HttpRequestMethod.GET;
        }
    }
}