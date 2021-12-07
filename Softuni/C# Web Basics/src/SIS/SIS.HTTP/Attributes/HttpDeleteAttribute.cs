using SIS.HTTP.Enums;

namespace SIS.HTTP.Attributes
{
    public class HttpDeleteAttribute : HttpRequestMethodAttribute
    {
        public HttpDeleteAttribute()
        {
        }

        public HttpDeleteAttribute(string url) : base(url)
        {
        }

        public override HttpRequestMethod RequestMethod => HttpRequestMethod.DELETE;
    }
}
