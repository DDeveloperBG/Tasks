using SIS.WebServer.DataManager;

namespace SIS.WebServer.MyViewEngine
{
    public class ErrorView : IView
    {
        private readonly string errorMessage;

        public ErrorView(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }

        public string ExecuteTemplate(object viewModel, IdentityUser user)
        {
            return errorMessage;
        }
    }
}
