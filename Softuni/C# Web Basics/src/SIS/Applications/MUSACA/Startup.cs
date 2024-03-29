﻿using Microsoft.EntityFrameworkCore;
using MUSACA.Data;
using MUSACA.Services.Orders;
using MUSACA.Services.Products;
using MUSACA.Services.Receipts;
using MUSACA.Services.Users;
using SIS.WebServer;
using SIS.WebServer.DependencyInversion;
using SIS.WebServer.Routing;

namespace MUSACA
{
    public class Startup : IMvcApplication
    {
        public void Configure(IServerRoutingTable routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IReceiptsService, ReceiptsService>();
            serviceCollection.Add<IOrdersService, OrdersService>();
            serviceCollection.Add<IProductsService, ProductsService>();
        }
    }
}
