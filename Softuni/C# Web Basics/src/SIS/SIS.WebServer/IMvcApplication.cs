using SIS.WebServer.DependencyInversion;
using SIS.WebServer.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.WebServer
{
    public interface IMvcApplication
    {
        void ConfigureServices(IServiceCollection serviceCollection);

        void Configure(IServerRoutingTable routeTable);
    }
}
