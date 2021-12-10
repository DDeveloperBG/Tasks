using MUSACA.ViewModels.Home;
using MUSACA.ViewModels.Products;
using System;

namespace MUSACA.Services.Products
{
    public interface IProductsService
    {
        public HomeProductsViewModel GetUserProducts(Guid userId);

        public AllProductsViewModel GetProducts();

        public void CreateProduct(ProductInputModel productInput);

        public int GetProductId(string barcode);
    }
}
