using SIS.HTTP.Enums;
using SIS.HTTP.Responses;
using SIS.WebServer.MyViewEngine;
using SIS.WebServer.Results;
using System.IO;
using System.Runtime.CompilerServices;

namespace SIS.WebServer.Controllers
{
    public class Controller
    {
        private readonly IViewEngine viewEngine;
        private static readonly string layout;
        private const string ViewPositionKeyword = "____VIEW_GOES_HERE____";

        static Controller()
        {
            layout = File.ReadAllText("Views/Shared/_Layout.html");
            layout = layout.Replace("@RenderBody()", ViewPositionKeyword);
        }

        public Controller()
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

        private string PutViewInLayout(string viewContent, object viewModel = null)
        {
            string processedLayout = viewEngine.GetHtml((string)layout.Clone(), viewModel);
            var responseHtml = processedLayout.Replace(ViewPositionKeyword, viewContent);

            return responseHtml;
        }
    }
}
