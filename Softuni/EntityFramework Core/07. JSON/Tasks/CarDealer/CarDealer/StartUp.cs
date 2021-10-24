using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext db = new CarDealerContext();

            Console.WriteLine(GetSalesWithAppliedDiscount(db));
        }

        private static string ReadFile(string fileName)
        {
            string path = @"D:\Downloads\Tasks\Softuni\EntityFramework Core\07. JSON\Tasks\CarDealer\CarDealer\Datasets\";
            return File.ReadAllText(path + fileName);
        }

        private static MapperConfiguration GetConfiguration() =>
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CarForTask10DTO, Car>();
                cfg.CreateMap<Customer, CarForTask15DTO>();
                cfg.CreateMap<Customer, CustomerForTask13DTO>();
            });

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(inputJson);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}.";
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            HashSet<int> suppliersIds = context.Suppliers.Select(x => x.Id).ToHashSet();

            var parts = JsonConvert.DeserializeObject<List<Part>>(inputJson)
                .Where(x => suppliersIds.Contains(x.SupplierId))
                .ToList();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}.";
        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            HashSet<int> partsIds = context.Parts.Select(x => x.Id).ToHashSet();

            var carsData = JsonConvert.DeserializeObject<List<CarForTask10DTO>>(inputJson)
                .Where(x => x.PartsId
                    .All(x => partsIds.Contains(x)));

            IMapper mapper = GetConfiguration().CreateMapper();

            var cars = carsData.ToDictionary(x => x, y => mapper.Map<Car>(y));
            var carParts = carsData.SelectMany(carData =>
            {
                var car = cars[carData];

                return carData.PartsId.Select(partId => new PartCar()
                {
                    Car = car,
                    PartId = partId
                });
            });

            context.Cars.AddRange(cars.Values);
            context.PartCars.AddRange(carParts);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}.";
        }

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<List<Customer>>(inputJson);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}.";
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<List<Sale>>(inputJson);

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}.";
        }

        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var configuration = GetConfiguration();

            var customers = context
                .Customers
                .ProjectTo<CustomerForTask13DTO>(configuration)
                .OrderBy(x => x.BirthDate)
                .ThenBy(x => !x.IsYoungDriver)
                .ToList();

            var settings = new JsonSerializerSettings
            {
                DateFormatString = "dd/MM/yyyy"
            };

            return JsonConvert.SerializeObject(customers, settings);
        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var configuration = GetConfiguration();

            var cars = context
                .Cars
                .Where(x => x.Make == "Toyota")
                .ProjectTo<CarForTask15DTO>(configuration)
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .ToList();

            return JsonConvert.SerializeObject(cars);
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context
                .Suppliers
                .Where(x => !x.IsImporter)
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    PartsCount = x.Parts.Count
                })
                .ToList();

            return JsonConvert.SerializeObject(suppliers);
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context
                .Cars
                .Select(x => new
                {
                    car = new
                    {
                        Make = x.Make,
                        Model = x.Model,
                        TravelledDistance = x.TravelledDistance
                    },
                    parts = x.PartCars
                        .Select(y => new
                        {
                            Name = y.Part.Name,
                            Price = $"{y.Part.Price:f2}"
                        })
                })
                .ToList();

            return JsonConvert.SerializeObject(cars);
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context
                .Customers
                .Where(x => x.Sales.Count > 0)
                .Select(x => new
                {
                    fullName = x.Name,
                    boughtCars = x.Sales.Count,
                    spentMoney = x.Sales
                        .Sum(x => x.Car.PartCars
                            .Sum(x => x.Part.Price))

                })
                .OrderByDescending(x => x.spentMoney)
                .ToList();

            return JsonConvert.SerializeObject(customers, Formatting.Indented);
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context
                .Sales
                .Select(x => new
                {
                    car = new
                    {
                        Make = x.Car.Make,
                        Model = x.Car.Model,
                        TravelledDistance = x.Car.TravelledDistance
                    },
                    customerName = x.Customer.Name,
                    Discount = x.Discount.ToString("f2"),
                    price = x.Car.PartCars.Sum(x => x.Part.Price).ToString("f2"),
                    priceWithDiscount = (x.Car.PartCars.Sum(x => x.Part.Price) * ((100 - x.Discount) / 100)).ToString("f2")
                })
                .Take(10)
                .ToList();

            return JsonConvert.SerializeObject(sales);
        }
    }
}