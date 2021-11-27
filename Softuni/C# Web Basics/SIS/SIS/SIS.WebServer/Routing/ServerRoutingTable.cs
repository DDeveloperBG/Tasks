using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using System;
using System.Collections.Generic;

namespace SIS.WebServer.Routing
{
    public class ServerRoutingTable : IServerRoutingTable
    {
        private readonly Dictionary<HttpRequestMethod, Dictionary<string, Func<IHttpRequest, IHttpResponse>>> routes;

        public ServerRoutingTable()
        {
            routes = new Dictionary<HttpRequestMethod, Dictionary<string, Func<IHttpRequest, IHttpResponse>>>
            {
                [HttpRequestMethod.GET] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                [HttpRequestMethod.POST] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                [HttpRequestMethod.PUT] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                [HttpRequestMethod.DELETE] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>()
            };
        }

        public void Add(HttpRequestMethod method, string path, Func<IHttpRequest, IHttpResponse> func)
        {
            if (!routes.ContainsKey(method))
            {
                throw new InternalServerErrorException();
            }

            if (routes[method].ContainsKey(path))
            {
                throw new InternalServerErrorException();
            }

            routes[method].Add(path, func);
        }

        public bool Contains(HttpRequestMethod requestMethod, string path)
        {
            return routes.ContainsKey(requestMethod) && routes[requestMethod].ContainsKey(path);
        }

        public Func<IHttpRequest, IHttpResponse> Get(HttpRequestMethod requestMethod, string path)
        {
            if (!routes.ContainsKey(requestMethod))
            {
                throw new BadRequestException();
            }

            if (!routes[requestMethod].ContainsKey(path))
            {
                throw new BadRequestException();
            }

            return routes[requestMethod][path];
        }
    }
}
