using SIS.WebServer.Controllers;
using SIS.WebServer.Routing;
using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading.Tasks;

namespace SIS.WebServer
{
    public class Server
    {
        private const string LocalhostIpAddress = "127.0.0.1";

        private readonly int port;

        private readonly TcpListener listener;

        private readonly IServerRoutingTable serverRoutingTable;

        private bool isRunning;

        public Server(int port, Assembly caller)
        {
            this.port = port;
            this.serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable
                .LoadControllers(caller)
                .LoadStaticFiles();

            listener = new TcpListener(IPAddress.Parse(LocalhostIpAddress), port);
        }

        public async Task RunAsync()
        {
            listener.Start();
            isRunning = true;

            Console.WriteLine($"Server started at http://{LocalhostIpAddress}:{port}");

            while (isRunning)
            {
                Console.WriteLine("Waiting for client...");

                var client = await listener.AcceptSocketAsync();

                ListenAsync(client);
            }
        }

        public async Task ListenAsync(Socket client)
        {
            var connectionHandler = new ConnectionHandler(client, serverRoutingTable);
            await connectionHandler.ProcessRequestAsync();
        }
    }
}