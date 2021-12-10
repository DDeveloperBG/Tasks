using MUSACA.Services.Receipts;
using MUSACA.Services.Users;
using MUSACA.ViewModels.Users;
using SIS.HTTP.Attributes;
using SIS.HTTP.Responses;
using SIS.WebServer.Authorization;
using SIS.WebServer.Controllers;
using SIS.WebServer.DataManager;
using System.ComponentModel.DataAnnotations;

namespace MUSACA.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IReceiptsService receiptsService;

        public UsersController(IUsersService usersService, IReceiptsService receiptsService)
        {
            this.usersService = usersService;
            this.receiptsService = receiptsService;
        }

        [GuestOnly]
        public IHttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        [GuestOnly]
        public IHttpResponse Login(LoginInputModel data)
        {
            IdentityUser user = this.usersService.GetUser(data.Username, data.Password);
            if (user == null)
            {
                return this.Error("Invalid username or password");
            }

            this.SignIn(user);
            return this.Redirect("/");
        }

        [GuestOnly]
        public IHttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        [GuestOnly]
        public IHttpResponse Register(RegisterInputModel input)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (input.Username == null)
            {
                return this.Error("Invalid username.");
            }

            if (string.IsNullOrWhiteSpace(input.Email) || !new EmailAddressAttribute().IsValid(input.Email))
            {
                return this.Error("Invalid email.");
            }

            if (input.Password == null)
            {
                return this.Error("Invalid password.");
            }

            if (input.Password != input.ConfirmPassword)
            {
                return this.Error("Passwords should be the same.");
            }

            if (!this.usersService.IsUsernameAvailable(input.Username))
            {
                return this.Error("Username already taken.");
            }

            if (!this.usersService.IsEmailAvailable(input.Email))
            {
                return this.Error("Email already taken.");
            }

            this.usersService.CreateUser(input.Username, input.Email, input.Password);
            return this.Redirect("/Users/Login");
        }

        [Authorize]
        public IHttpResponse Logout()
        {
            this.SignOut();

            return this.Redirect("/");
        }

        [Authorize]
        public IHttpResponse Profile()
        {
            ProfileViewModel viewModel = new ProfileViewModel();
            viewModel.Receipts = receiptsService.GetUserReceipts(this.GetUserId());

            return this.View(viewModel);
        }
    }
}
