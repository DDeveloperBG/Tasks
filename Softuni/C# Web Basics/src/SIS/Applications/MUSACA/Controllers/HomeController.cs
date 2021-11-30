using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Controllers;
using SIS.WebServer.Results;

namespace Demo
{
    public class HomeController : Controller
    {
        public IHttpResponse Index(IHttpRequest request)
        {
            string content = "<h1>Hello, World!</h1>";

            return new HtmlResult(content, HttpResponseStatusCode.Ok);
        }
    }
}
