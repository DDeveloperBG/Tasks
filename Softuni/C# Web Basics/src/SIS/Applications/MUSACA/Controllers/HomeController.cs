using SIS.HTTP.Attributes;
using SIS.HTTP.Responses;
using SIS.WebServer.Controllers;

namespace Demo
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IHttpResponse Index()
        {
            return this.View();
        }
    }
}
