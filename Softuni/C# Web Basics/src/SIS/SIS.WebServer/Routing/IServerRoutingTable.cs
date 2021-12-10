using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using System;
using System.Collections.Generic;

namespace SIS.WebServer.Routing
{
    public interface IServerRoutingTable
    {
        public void Add(HttpRequestMethod method, string path, Func<IHttpRequest, IHttpResponse> func);

        public bool Contains(HttpRequestMethod requestMethod, string path);

        public List<string> GetAllRouteNames();

        Func<IHttpRequest, IHttpResponse> Get(HttpRequestMethod requestMethod, string path);
    }
}
