using SIS.WebServer;
using System.Threading.Tasks;

namespace Demo
{
    class Launcher
    {
        static async Task Main()
        {         
            Server server = new Server(81, typeof(Launcher).Assembly);

            await server.RunAsync();
        }
    }
}
