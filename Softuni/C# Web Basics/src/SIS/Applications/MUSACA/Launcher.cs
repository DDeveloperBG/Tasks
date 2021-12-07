using SIS.WebServer;
using System.Threading.Tasks;

namespace MUSACA
{
    class Launcher
    {
        static async Task Main()
        {         
            await Server.RunAsync(new Startup(), 81);
        }
    }
}
