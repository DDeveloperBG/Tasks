using SIS.WebServer.DataManager;

namespace SIS.WebServer.MyViewEngine
{
    public interface IViewEngine
    {
        string GetHtml(string templateCode, object viewModel, IdentityUser user);
    }
}
