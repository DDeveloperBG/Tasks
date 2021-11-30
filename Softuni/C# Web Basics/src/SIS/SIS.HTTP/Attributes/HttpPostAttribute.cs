using SIS.HTTP.Enums;

namespace SIS.HTTP.Attributes
{
    public class HttpPostAttribute : HttpRequestMethodAttribute
    {
        public HttpPostAttribute()
        {
            RequestMethod = HttpRequestMethod.POST;
        }
    }
}