using SIS.HTTP.Enums;

namespace SIS.HTTP.Attributes
{
    public class HttpDeleteAttribute : HttpRequestMethodAttribute
    {
        public HttpDeleteAttribute()
        {
            RequestMethod = HttpRequestMethod.DELETE;
        }
    }
}
