using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIS.WebServer.Routing
{
    public class ServerRoutingTable : IServerRoutingTable
    {
        private readonly List<Route> routes;

        public ServerRoutingTable()
        {
            this.routes = new List<Route>();
        }

        public void Add(HttpRequestMethod method, string path, Func<IHttpRequest, IHttpResponse> func)
        {
            if (this.Contains(method, path))
            {
                throw new InternalServerErrorException();
            }

            this.routes.Add(new Route(method, path, func));
        }

        public bool Contains(HttpRequestMethod method, string path)
        {
            return routes.Any(x => x.HasEqual(method, path));
        }

        public Func<IHttpRequest, IHttpResponse> Get(HttpRequestMethod requestMethod, string path)
        {
            var route = routes.First(x => x.HasEqual(requestMethod, path));

            if (route == null)
            {
                throw new BadRequestException();
            }

            return route.Action;
        }
    }
}
