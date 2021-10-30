using System.Linq;
using CarDealer.Xml;
using CarDealer.Data;
using CarDealer.Models;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            var db = new CarDealerContext();
            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();

            //ImportSuppliers(db, Reader.ReadFrom("suppliers.xml"));
            //ImportParts(db, Reader.ReadFrom("parts.xml"));
            //ImportCars(db, Reader.ReadFrom("cars.xml"));
            //ImportCustomers(db, Reader.ReadFrom("customers.xml"));
            //ImportSales(db, Reader.ReadFrom("sales.xml"));

            string result = GetSalesWithAppliedDiscount(db);
            System.Console.WriteLine(result);
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var suppliersDto = XmlApplier
                .Deserialize<DTOs.Export.SupplierDTO>(inputXml, "Suppliers");

            var suppliers = MapperApplier
                .MapCollection<DTOs.Export.SupplierDTO, Supplier>(suppliersDto);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}";
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            HashSet<int> usersIds = context.Suppliers
                .Select(x => x.Id)
                .ToHashSet();

            var partsDto = XmlApplier
                .Deserialize<DTOs.Import.PartDTO>(inputXml, "Parts")
                .Where(x => usersIds.Contains(x.SupplierId));

            var parts = MapperApplier
                .MapCollection<DTOs.Import.PartDTO, Part>(partsDto);

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Length}";
        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var carsDto = XmlApplier
                .Deserialize<DTOs.Import.CarDTO>(inputXml, "Cars");

            var cars = MapperApplier.MapCollection<DTOs.Import.CarDTO, Car>(carsDto);

            context.Cars.AddRange(cars);
            context.SaveChanges();

            for (int i = 0; i < cars.Length; i++)
            {
                int carId = cars[i].Id;

                var carParts = carsDto[i].Parts
                    .Select(x => new PartCar
                    {
                        PartId = x.Id,
                        CarId = carId
                    });

                foreach (var part in carParts)
                {
                    cars[i].PartCars.Add(part);
                }
            }

            context.SaveChanges();

            return $"Successfully imported {cars.Length}";
        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var customersDto = XmlApplier
                .Deserialize<CustommerDTO>(inputXml, "Customers");

            var customers = MapperApplier
                .MapCollection<CustommerDTO, Customer>(customersDto);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Length}";
        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            HashSet<int> carsIds = context.Cars.Select(x => x.Id).ToHashSet();

            var salesDto = XmlApplier
                .Deserialize<DTOs.Import.SaleDTO>(inputXml, "Sales")
                .Where(x => carsIds.Contains(x.CarId));

            var sales = MapperApplier
                .MapCollection<DTOs.Import.SaleDTO, Sale>(salesDto);

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Length}";
        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var cars = context
                .Cars
                .Where(x => x.TravelledDistance > 2_000_000)
                .ProjectTo<DTOs.Export.Task14CarDTO>(MapperApplier.Configuration)
                .OrderBy(x => x.Make)
                .ThenBy(x => x.Model)
                .Take(10)
                .ToArray();

            return XmlApplier.SerializeCollection(cars, "cars");
        }

        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var cars = context
                 .Cars
                 .Where(x => x.Make == "BMW")
                 .OrderBy(x => x.Model)
                 .ThenByDescending(x => x.TravelledDistance)
                 .ProjectTo<Task15CarDTO>(MapperApplier.Configuration)
                 .ToArray();

            return XmlApplier.SerializeCollection(cars, "cars");
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context
                .Suppliers
                .Where(x => !x.IsImporter)
                .ProjectTo<DTOs.Export.SupplierDTO>(MapperApplier.Configuration)
                .ToArray();

            return XmlApplier.SerializeCollection(suppliers, "suppliers");
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context
                 .Cars
                 .OrderByDescending(x => x.TravelledDistance)
                 .ThenBy(x => x.Model)
                 .ProjectTo<Task17CarDTO>(MapperApplier.Configuration)
                 .Take(5)
                 .ToArray();

            return XmlApplier.SerializeCollection(cars, "cars");
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context
                .Customers
                .Where(x => x.Sales.Count > 0)
                .Select(x => new CustomerDTO
                {
                    FullName = x.Name,
                    BoughtCarsCount = x.Sales.Count,
                    SpentMoney = x.Sales.Sum(s => s.Car.PartCars.Sum(p => p.Part.Price))
                })
                .OrderByDescending(x => x.SpentMoney)
                .ToArray();

            return XmlApplier.SerializeCollection(customers, "customers");
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context
                .Sales
                .ProjectTo<DTOs.Export.SaleDTO>(MapperApplier.Configuration)
                .ToArray();

            return XmlApplier.SerializeCollection(sales, "sales");
        }
    }
}