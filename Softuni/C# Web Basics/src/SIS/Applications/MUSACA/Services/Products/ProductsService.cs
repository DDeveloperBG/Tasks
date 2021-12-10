using MUSACA.Data;
using MUSACA.Data.Enums;
using MUSACA.Data.Models;
using MUSACA.ViewModels.Home;
using MUSACA.ViewModels.Products;
using System;
using System.Linq;

namespace MUSACA.Services.Products
{
    public class ProductsService : IProductsService
    {
        private ApplicationDbContext db;

        public ProductsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public HomeProductsViewModel GetUserProducts(Guid userId)
        {
            return db.Users
                .Where(x => x.Id == userId)
                .Select(x => new HomeProductsViewModel
                {
                    Products = x.Orders
                        .Where(y => y.Status == OrderStatus.Active)
                        .Select(y => new HomeProductViewModel
                        {
                            Name = y.Product.Name,
                            Price = y.Product.Price,
                            Quantity = y.Quantity,
                        })
                        .ToList()
                })
                .First();
        }

        public AllProductsViewModel GetProducts()
        {
            var products = new AllProductsViewModel();

            products.Products = db.Products
                .Select(x => new ProductViewModel
                {
                    Name = x.Name,
                    Price = x.Price,
                    Barcode = x.Barcode,
                    Image = x.PictureUrl
                })
                .ToList();

            return products;
        }

        public void CreateProduct(ProductInputModel productInput)
        {
            var product = new Product
            {
                Name = productInput.Name,
                Price = productInput.Price,
                Barcode = productInput.Barcode,
                PictureUrl = productInput.Picture
            };

            db.Products.Add(product);

            db.SaveChanges();
        }

        public int GetProductId(string barcode)
        {
            return db.Products
                .Where(x => x.Barcode == barcode)
                .Select(x => x.Id)
                .FirstOrDefault();
        }
    }
}
