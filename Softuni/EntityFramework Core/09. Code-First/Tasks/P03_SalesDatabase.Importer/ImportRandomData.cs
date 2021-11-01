using System;
using System.Collections.Generic;
using P03_SalesDatabase.Data;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Importer
{
    public static class ImportRandomData
    {
        private static int Times { get; set; }
        private static SalesContext Db { get; set; }

        private static readonly List<string> ProductNames = new List<string>
        {
            "Avocado", "Watercress", "Sandwich", "Flan", "Calamari", "Raspberry", "Lemon", "Meringue", "Pie", "Baked", "Potato", "Soup", "Oysters", "Rockefeller", "Sticky", "Toffee", "Pudding", "Chicken", "Fried", "Steak", "Cinnamon", "Bread", "Maple", "Bacon", "Doughnut", "Bagel", "and", "Lox", "Persimmon", "Eggplant", "Udon", "Hibiscus", "Tea", "Cactus", "Fries", "Pomelo", "Jumbalaya", "Chicken", "Noodle", "Soup", "Pho", "Black", "Forest", "Cake", "Butter", "Chicken", "Philly", "Cheese", "Steak", "Fettucini", "Alfredo", "Spaghetti", "Squash", "Frittata", "Masala", "Dosa", "Eel", "Profiteroles", "Escargots", "Cream", "Cheese", "Frosting", "Pineapple", "Zucchini", "Flowers", "Arugula", "Blackberry", "Salad", "Dragonfruit", "Carbonara", "Chia", "Pudding", "Mango", "Lassi", "Corned", "Beef", "Sandwich", "BLT", "Bubble", "Tea", "Chocolate", "Raspberry", "Brownies", "Clam", "Cakes", "Lamb", "Chops", "Smith", "Island", "Cake", "Cheese", "Stuffed", "Jalapenos", "Alligator", "Mexican", "Street", "Corn", "Chow", "Mein", "Corn", "Chowder", "Peanut", "Butter,", "Banana,", "and", "Bacon", "Tennessee", "Coconut", "Cream", "Pie", "Huevos", "Rancheros"
        };
        private static readonly List<string> HumanNames = new List<string>
        {
            "James", "Robert", "John", "Michael", "William", "David", "Richard", "Joseph", "Thomas", "Charles", "Christopher", "Daniel", "Matthew", "Anthony", "Mark", "Donald", "Steven", "Paul", "Andrew", "Joshua", "Kenneth", "Kevin", "Brian", "Mary", "Patricia", "Jennifer", "Linda", "Elizabeth", "Barbara", "Susan", "Jessica", "Sarah", "Karen", "Nancy", "Lisa", "Betty", "Margaret", "Sandra", "Ashley", "Kimberly", "Emily", "Donna", "Michelle", "Dorothy", "Carol", "Amanda"
        };
        private static readonly List<string> StoreNames = new List<string>
        {
            "Billa", "Lidl", "Walmart", "Amazon", "Kaufland", "Penny Markt", "Globus", "Metro"
        };

        public static void To(SalesContext db, int times)
        {
            Db = db;
            Times = times;

            AddCustomers();
            AddProducts();
            AddStores();
            AddSales();
        }

        private static void AddProducts()
        {
            Random rand = new Random();

            for (int i = 0; i < Times; i++)
            {
                string name = ProductNames[rand.Next(0, ProductNames.Count)];
                int quantity = rand.Next(1, 100);
                int price = rand.Next(1, 2200);

                var product = new Product
                {
                    Name = name,
                    Quantity = quantity,
                    Price = price,
                };

                Db.Products.Add(product);
            }

            Db.SaveChanges();
        }

        private static void AddCustomers()
        {
            Random rand = new Random();

            for (int i = 0; i < Times; i++)
            {
                string name = HumanNames[rand.Next(0, HumanNames.Count)];

                var customer = new Customer
                {
                    Name = name,
                };

                Db.Customers.Add(customer);
            }

            Db.SaveChanges();
        }

        private static void AddStores()
        {
            Random rand = new Random();

            for (int i = 0; i < Times; i++)
            {
                string name = StoreNames[rand.Next(0, StoreNames.Count)];

                var store = new Store
                {
                    Name = name,
                };

                Db.Stores.Add(store);
            }

            Db.SaveChanges();
        }

        private static void AddSales()
        {
            Random rand = new Random();

            for (int i = 0; i < Times / 2; i++)
            {
                DateTime dateTime = DateTime.Now;
                int productId = rand.Next(1, Times);
                int customerId = rand.Next(1, Times);
                int storeId = rand.Next(1, Times);

                var sale = new Sale
                {
                    Date = dateTime,
                    ProductId = productId,
                    CustomerId = customerId,
                    StoreId = storeId
                };

                Db.Sales.Add(sale);
            }

            Db.SaveChanges();
        }
    }
}