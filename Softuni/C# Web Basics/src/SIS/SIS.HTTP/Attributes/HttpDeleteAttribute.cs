using SIS.HTTP.Enums;

namespace SIS.HTTP.Attributes
{
    public class HttpDeleteAttribute : HttpRequestMethodAttribute
    {
        public HttpDeleteAttribute(string )
        {
        }

        public override HttpRequestMethod RequestMethod { get => HttpRequestMethod.DELETE; }
    }
}
