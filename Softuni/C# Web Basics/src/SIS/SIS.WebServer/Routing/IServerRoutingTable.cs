using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using System;

namespace SIS.WebServer.Routing
{
    public interface IServerRoutingTable
    {
        public void Add(HttpRequestMethod method, string path, Func<IHttpRequest, IHttpResponse> func);

        public bool Contains(HttpRequestMethod requestMethod, string path);

        Func<IHttpRequest, IHttpResponse> Get(HttpRequestMethod requestMethod, string path);
    }
}
