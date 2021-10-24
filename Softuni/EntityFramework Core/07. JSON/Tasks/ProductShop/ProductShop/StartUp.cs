using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            var db = new ProductShopContext();

            Console.WriteLine(GetCategoriesByProductsCount(db));
        }

        private static string ReadJson(string file)
        {
            string path = @"D:\Downloads\Tasks\Softuni\EntityFramework Core\07. JSON\Tasks\ProductShop\ProductShop\Datasets\";
            return File.ReadAllText(path + file);
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<User[]>(inputJson);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<Category[]>(inputJson)
                .Where(x => x.Name != null)
                .ToList();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Length}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var data = context.Products
                 .Where(x => x.Price >= 500 && x.Price <= 1000)
                 .OrderBy(x => x.Price)
                 .Select(x => new
                 {
                     x.Name,
                     x.Price,
                     Seller = x.Seller.FirstName + " " + x.Seller.LastName
                 })
                 .ToList();

            var serializeSetting = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            return JsonConvert.SerializeObject(data, serializeSetting);
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(x => x.ProductsSold.Any(x => x.BuyerId != null))
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    SoldProducts = x.ProductsSold
                        .Where(x => x.BuyerId != null)
                        .Select(x => new
                        {
                            x.Name,
                            x.Price,
                            BuyerFirstName = x.Buyer.FirstName,
                            BuyerLastName = x.Buyer.LastName
                        })
                })
                .ToList();

            var serializeSetting = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            return JsonConvert.SerializeObject(users, serializeSetting);
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(x => new
                {
                    x.Name,
                    ProductsCount = x.CategoryProducts.Count,
                    AveragePrice = $"{x.CategoryProducts.Average(x => x.Product.Price):f2}",
                    TotalRevenue = $"{x.CategoryProducts.Sum(x => x.Product.Price):f2}"
                })
                .OrderByDescending(x => x.ProductsCount)
                .ToList();

            var serializeSetting = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };

            return JsonConvert.SerializeObject(categories, serializeSetting);
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(x => x.ProductsSold.Any(x => x.BuyerId != null))
                .OrderByDescending(x => x.ProductsSold.Count)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Age,
                    SoldProducts = new
                    {
                        Count = x.ProductsSold.Count,
                        Products = x.ProductsSold
                            .Select(y => new
                            {
                                y.Name,
                                y.Price
                            })
                    }
                })
                .ToList();

            var serializeSetting = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

            var data = new
            {
                usersCount = users.Count(),
                users
            };

            return JsonConvert.SerializeObject(data, serializeSetting);
        }
    }
}