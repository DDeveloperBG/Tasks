using MUSACA.Services.Products;
using MUSACA.ViewModels.Home;
using SIS.HTTP.Attributes;
using SIS.HTTP.Responses;
using SIS.WebServer.Controllers;

namespace Demo
{
    public class HomeController : Controller
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet("/")]
        public IHttpResponse Index()
        {
            var model = new HomeProductsViewModel();

            if (this.IsUserSignedIn())
            {
                var userId = this.GetUserId();

                model = productsService.GetUserProducts(userId);
            }

            return this.View(model);
        }
    }
}
