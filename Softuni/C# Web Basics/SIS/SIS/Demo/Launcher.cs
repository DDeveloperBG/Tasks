using SIS.HTTP.Enums;
using SIS.HTTP.Responses;
using SIS.WebServer;
using SIS.WebServer.Results;
using SIS.WebServer.Routing;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Demo
{
    class Launcher
    {
        static async Task Main()
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            var homeController = new HomeController();
            var dogsController = new DogsController();

            serverRoutingTable.Add(
                    HttpRequestMethod.GET,
                    "/",
                    request => homeController.Index(request)
                );

            //serverRoutingTable.Add(
            //        HttpRequestMethod.GET,
            //        "/favicon.ico",
            //        request => GetFavIcon()
            //    );

            serverRoutingTable.Add(
                 HttpRequestMethod.GET,
                 "/Dog",
                 request => dogsController.AllDogs(request)
             );

            serverRoutingTable.Add(
                 HttpRequestMethod.GET,
                 "/Dog/Add",
                 request => dogsController.AddDogForm(request)
             );

            serverRoutingTable.Add(
                 HttpRequestMethod.POST,
                 "/Dog/Add",
                 request => dogsController.AddDog(request)
             );

            Server server = new Server(81, serverRoutingTable);

            await server.RunAsync().ConfigureAwait(false);
        }

        private static IHttpResponse GetFavIcon()
        {
            string contentType = "image/x-icon";

            byte[] content = File.ReadAllBytes("./Resourses/Icon.ico");

            return new ByteResult(content, HttpResponseStatusCode.Ok, contentType);
        }
    }
}
