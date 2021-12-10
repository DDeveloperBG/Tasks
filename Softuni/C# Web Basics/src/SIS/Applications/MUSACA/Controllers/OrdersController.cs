using MUSACA.Services.Orders;
using SIS.HTTP.Attributes;
using SIS.HTTP.Responses;
using SIS.WebServer.Authorization;
using SIS.WebServer.Controllers;

namespace MUSACA.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        [Authorize]
        public IHttpResponse Cashout()
        {
            ordersService.CreateReceipt(this.GetUserId());

            return this.Redirect("/");
        }
    }
}
