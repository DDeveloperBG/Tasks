using SIS.HTTP.Enums;

namespace SIS.HTTP.Attributes
{
    public class HttpPutAttribute : HttpRequestMethodAttribute
    {
        public HttpPutAttribute()
        {
            RequestMethod = HttpRequestMethod.PUT;
        }
    }
}
