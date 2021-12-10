using SIS.WebServer.Controllers;
using SIS.WebServer.DependencyInversion;
using SIS.WebServer.Routing;
using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading.Tasks;

namespace SIS.WebServer
{
    public static class Server
    {
        private const string LocalhostIpAddress = "127.0.0.1";

        private static IServerRoutingTable ServerRoutingTable;

        private static bool IsRunning;

        public static async Task RunAsync(IMvcApplication application, int port)
        {
            ServerRoutingTable = new ServerRoutingTable();
            IServiceCollection serviceCollection = new ServiceCollection();

            application.ConfigureServices(serviceCollection);
            application.Configure(ServerRoutingTable);

            ServerRoutingTable
                .LoadControllers(application.GetType().Assembly, serviceCollection)
                .LoadStaticFiles();

            Console.WriteLine(string.Join(Environment.NewLine, ServerRoutingTable.GetAllRouteNames()));

            TcpListener listener = new TcpListener(IPAddress.Parse(LocalhostIpAddress), port);

            listener.Start();
            IsRunning = true;

            Console.WriteLine($"Server started at http://{LocalhostIpAddress}:{port}");

            while (IsRunning)
            {
                Console.WriteLine("Waiting for client...");

                var client = await listener.AcceptSocketAsync();

                ListenAsync(client);
            }
        }

        private static async Task ListenAsync(Socket client)
        {
            var connectionHandler = new ConnectionHandler(client, ServerRoutingTable);
            await connectionHandler.ProcessRequestAsync();
        }
    }
}