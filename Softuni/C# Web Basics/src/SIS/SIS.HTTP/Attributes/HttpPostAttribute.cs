using SIS.HTTP.Enums;

namespace SIS.HTTP.Attributes
{
    public class HttpPostAttribute : HttpRequestMethodAttribute
    {
        public HttpPostAttribute()
        {
        }

        public HttpPostAttribute(string url) : base(url)
        {
        }

        public override HttpRequestMethod RequestMethod => HttpRequestMethod.POST;
    }
}