using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.HTTP.Sessions;
using SIS.WebServer.MyViewEngine;
using SIS.WebServer.Results;
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
        private const string UserIdSessionName = "UserId";

        static Controller()
        {
            layout = File.ReadAllText("Views/Shared/_Layout.html");
            layout = layout.Replace("@Body", ViewPositionKeyword);
        }

        protected Controller()
        {
            viewEngine = new ViewEngine();
        }

        protected HttpResponse View(
            object viewModel = null,
            [CallerMemberName] string viewPath = null)
        {
            var viewContent = File.ReadAllText(
                "Views/" +
                GetType().Name.Replace("Controller", string.Empty) +
                "/" + viewPath + ".html");

            viewContent = viewEngine.GetHtml(viewContent, viewModel);

            var responseHtml = PutViewInLayout(viewContent, viewModel);

            var response = new HtmlResult(responseHtml, HttpResponseStatusCode.Ok);
            return response;
        }

        protected HttpResponse Error(string errorText)
        {
            var viewContent = $"<div class=\"alert alert-danger\" role=\"alert\">{errorText}</div>";
            var responseHtml = this.PutViewInLayout(viewContent);
            var response = new HtmlResult(responseHtml, HttpResponseStatusCode.BadRequest);

            return response;
        }

        protected HttpResponse Redirect(string url)
        {
            return new RedirectResult(url);
        }

        protected void SignIn(IHttpRequest request, string userId)
        {
            HttpSessionStorage.GetSession(GetUserSessionId(request))
                .AddParameter(UserIdSessionName, userId);
        }

        protected void SignOut(IHttpRequest request)
        {
            HttpSessionStorage.RemoveSession(GetUserSessionId(request));
        }

        protected bool IsUserSignedIn(IHttpRequest request)
        {
            return HttpSessionStorage.ContainsSession(GetUserSessionId(request));
        }

        protected string GetUserId(IHttpRequest request)
        {
            return HttpSessionStorage.GetSession(GetUserSessionId(request))
                .GetParameter(UserIdSessionName) as string;
        }

        private string GetUserSessionId(IHttpRequest request)
        {
            return request.Cookies.GetCookie(HttpSessionStorage.SessionCookieKey).Value;
        }

        private string PutViewInLayout(string viewContent, object viewModel = null)
        {
            string processedLayout = viewEngine.GetHtml((string)layout.Clone(), viewModel);
            var responseHtml = processedLayout.Replace(ViewPositionKeyword, viewContent);

            return responseHtml;
        }
    }
}
