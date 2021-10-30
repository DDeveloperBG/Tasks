using System;
using System.Linq;
using ProductShop.IO;
using ProductShop.DTOs;
using ProductShop.Data;
using ProductShop.Models;
using ProductShop.XmlController;
using ProductShop.MapperConfigs;
using AutoMapper.QueryableExtensions;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            var db = new ProductShopContext();
            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();

            //ImportUsers(db, Reader.ReadFrom("users.xml"));
            //ImportProducts(db, Reader.ReadFrom("products.xml"));
            //ImportCategories(db, Reader.ReadFrom("categories.xml"));
            //ImportCategoryProducts(db, Reader.ReadFrom("categories-products.xml"));

            string result = GetUsersWithProducts(db);
            Console.WriteLine(result);
        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var usersInputData = XmlApplier
                .Deserialize<Task1UserDTO>("Users", inputXml);

            var users = MapperApplier
                .MapCollection<Task1UserDTO, User>(usersInputData);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count}";
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var productsInputData = XmlApplier
                .Deserialize<Task2ProductDTO>("Products", inputXml);

            var products = MapperApplier
                .MapCollection<Task2ProductDTO, Product>(productsInputData);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count}";
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var categoriesInputData = XmlApplier
                .Deserialize<Task3CategoryDTO>("Categories", inputXml);

            var categories = MapperApplier
                .MapCollection<Task3CategoryDTO, Category>(categoriesInputData);

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var categoryProductsInput = XmlApplier
                .Deserialize<Task4CategoryProductDTO>("CategoryProducts", inputXml);

            var categoryProducts = MapperApplier
                .MapCollection<Task4CategoryProductDTO, CategoryProduct>(categoryProductsInput);

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context
                .Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .ProjectTo<Task5ProductDTO>(MapperApplier.MapperConfiguration)
                .OrderBy(x => x.Price)
                .Take(10)
                .ToArray();

            return XmlApplier.SerializeMany("Products", products);
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context
                .Users
                .Where(x => x.ProductsSold.Any(x => x.Buyer != null))
                .ProjectTo<Task6UserDTO>(MapperApplier.MapperConfiguration)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Take(5)
                .ToArray();

            return XmlApplier.SerializeMany("Users", users);
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context
                .Categories
                .ProjectTo<Task7CategoryDTO>(MapperApplier.MapperConfiguration)
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.TotalRevenue)
                .ToArray();

            return XmlApplier.SerializeMany("Categories", categories);
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var usersArray = context
                .Users
                //.ToArray() <- For Judge to work
                .Where(x => x.ProductsSold.Count > 0)
                .OrderByDescending(x => x.ProductsSold.Count)
                .Select(x => new Task8UserDTO
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age,
                    SoldProducts = new Task8SoldProductsDTO
                    {
                        Count = x.ProductsSold.Count,
                        products = MapperApplier
                            .MapCollection<Product, Task6ProductDTO>(x.ProductsSold)
                            .ToArray()
                    }
                })
                .Take(10)
                .ToArray();

            var usersObject = MapperApplier.MapElement<Task8UserDTO[], Task8UsersDTO>(usersArray);

            return XmlApplier.SerializeOne(usersObject);
        }
    }
}