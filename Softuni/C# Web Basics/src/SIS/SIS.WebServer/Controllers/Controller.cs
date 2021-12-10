using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.HTTP.Sessions;
using SIS.WebServer.DataManager;
using SIS.WebServer.MyViewEngine;
using SIS.WebServer.Results;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace SIS.WebServer.Controllers
{
    public abstract class Controller
    {
        private readonly IViewEngine viewEngine;
        private static readonly string layout;
        private const string ViewPositionKeyword = "____VIEW_GOES_HERE____";
        private const string UserKeyName = "User";

        static Controller()
        {
            layout = File.ReadAllText("Views/Shared/_Layout.html");
            layout = layout.Replace("@Body", ViewPositionKeyword);
        }

        protected Controller()
        {
            viewEngine = new ViewEngine();
        }

        public HttpResponse Redirect(string url)
        {
            return new RedirectResult(url);
        }

        public bool IsUserSignedIn()
        {
            string sessionId = GetUserSessionId();
            if (HttpSessionStorage.ContainsSession(sessionId))
            {
                var session = HttpSessionStorage.GetSession(sessionId);

                return session.ContainsParameter(UserKeyName);
            }

            return false;
        }

        protected HttpResponse View(
            object viewModel = null,
            [CallerMemberName] string viewPath = null)
        {
            var viewContent = File.ReadAllText(
                "Views/" +
                GetType().Name.Replace("Controller", string.Empty) +
                "/" + viewPath + ".html");

            IdentityUser user = GetUser();
            viewContent = viewEngine.GetHtml(viewContent, viewModel, user);

            var responseHtml = PutViewInLayout(viewContent, user, viewModel);

            var response = new HtmlResult(responseHtml, HttpResponseStatusCode.Ok);
            return response;
        }

        public IHttpRequest Request { get; set; }

        protected HttpResponse Error(string errorText)
        {
            var viewContent = $"<div class=\"alert alert-danger\" role=\"alert\">{errorText}</div>";
            var responseHtml = this.PutViewInLayout(viewContent, GetUser());
            var response = new HtmlResult(responseHtml, HttpResponseStatusCode.BadRequest);

            return response;
        }

        protected void SignIn(IdentityUser user)
        {
            HttpSessionStorage
                .GetSession(GetUserSessionId())
                .AddParameter(UserKeyName, user);
        }

        protected void SignOut()
        {
            HttpSessionStorage.RemoveSession(GetUserSessionId());
        }

        protected Guid GetUserId()
        {
            return GetUser().Id;
        }

        protected IdentityUser GetUser()
        {
            if (IsUserSignedIn())
            {
                return HttpSessionStorage
                    .GetSession(GetUserSessionId())
                    .GetParameter(UserKeyName) as IdentityUser;
            }

            return null;
        }

        private string GetUserSessionId()
        {
            return Request
                .Cookies
                .GetCookie(HttpSessionStorage.SessionCookieKey)
                .Value;
        }

        private string PutViewInLayout(string viewContent, IdentityUser user, object viewModel = null)
        {
            string processedLayout = viewEngine.GetHtml((string)layout.Clone(), viewModel, user);
            var responseHtml = processedLayout.Replace(ViewPositionKeyword, viewContent);

            return responseHtml;
        }
    }
}
