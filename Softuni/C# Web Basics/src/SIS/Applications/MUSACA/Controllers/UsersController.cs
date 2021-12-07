using MUSACA.ViewModels.Users;
using SIS.HTTP.Attributes;
using SIS.HTTP.Responses;
using SIS.WebServer.Controllers;

namespace MUSACA.Controllers
{
    public class UsersController : Controller
    {
        public UsersController()
        {
        }

        public IHttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public IHttpResponse Login(LoginInputModel data)
        {

            return this.Redirect("/");
        }
    }
}
