using SIS.WebServer;
using SIS.WebServer.Controllers;
using System.Threading.Tasks;

namespace Demo
{
    class Launcher
    {
        static async Task Main()
        {
            var serverRoutingTable = new ControllersManager()
                .LoadControllers(typeof(Launcher).Assembly);

            Server server = new Server(81, serverRoutingTable);

            await server.RunAsync();
        }
    }
}
