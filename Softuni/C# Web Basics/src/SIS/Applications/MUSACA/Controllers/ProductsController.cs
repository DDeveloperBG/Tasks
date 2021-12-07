using MUSACA.Services.Products;
using SIS.WebServer.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MUSACA.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

    }
}
