using MUSACA.Services.Orders;
using MUSACA.Services.Products;
using MUSACA.ViewModels.Orders;
using MUSACA.ViewModels.Products;
using SIS.HTTP.Attributes;
using SIS.HTTP.Responses;
using SIS.WebServer.Authorization;
using SIS.WebServer.Controllers;
using SIS.WebServer.DataManager;
using System;

namespace MUSACA.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;
        private readonly IOrdersService ordersService;

        public ProductsController(IProductsService productsService, IOrdersService ordersService)
        {
            this.productsService = productsService;
            this.ordersService = ordersService;
        }

        [Authorize]
        public IHttpResponse All()
        {
            return this.View(productsService.GetProducts());
        }

        [Authorize]
        public IHttpResponse Create()
        {
            if (GetUser().Role != UserRole.Admin)
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IHttpResponse Create(ProductInputModel productInput)
        {
            if (GetUser().Role != UserRole.Admin)
            {
                return this.Redirect("/");
            }

            if (productInput.Barcode.Length != 12)
            {
                return this.Error("Barcode shoud be 12 digits long!");
            }

            productsService.CreateProduct(productInput);

            return this.Redirect("/products/all");
        }

        [HttpPost]
        [Authorize]
        public IHttpResponse Order(OrderInputModel orderInput)
        {
            if (orderInput.Barcode.Length != 12)
            {
                return this.Error("Barcode shoud be 12 digits long!");
            }

            if (orderInput.Quantity < 1)
            {
                return this.Error("Quantity shoud be possitive number!");
            }

            int productId = productsService.GetProductId(orderInput.Barcode);

            if (productId == 0)
            {
                return this.Error("Not existing product barcode!");
            }

            Guid cashierId = this.GetUserId();

            ordersService.Create(productId, orderInput.Quantity, cashierId);

            return this.Redirect("/");
        }
    }
}
