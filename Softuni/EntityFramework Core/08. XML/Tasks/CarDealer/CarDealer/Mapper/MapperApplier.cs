using AutoMapper;
using System.Linq;
using CarDealer.Models;
using CarDealer.DTOs.Import;
using CarDealer.DTOs.Export;
using System.Collections.Generic;

namespace CarDealer
{
    public static class MapperApplier
    {
        public static readonly IConfigurationProvider Configuration
            = new MapperConfiguration(e =>
            {
                e.CreateMap<DTOs.Import.SupplierDTO, Supplier>();
                e.CreateMap<DTOs.Import.PartDTO, Part>();
                e.CreateMap<DTOs.Import.CarDTO, Car>();
                e.CreateMap<CustommerDTO, Customer>();
                e.CreateMap<DTOs.Import.SaleDTO, Sale>();

                e.CreateMap<Car, Task14CarDTO>();
                e.CreateMap<Car, Task15CarDTO>();
                e.CreateMap<Supplier, DTOs.Export.SupplierDTO>()
                    .ForMember(x => x.PartsCount, y => y
                        .MapFrom(s => s.Parts.Count));

                e.CreateMap<Car, Task17CarDTO>()
                    .ForMember(x => x.Parts, y => y
                          .MapFrom(s => s.PartCars
                              .OrderByDescending(x => x.Part.Price)
                              .Select(x => new DTOs.Export.PartDTO
                              {
                                  Name = x.Part.Name,
                                  Price = x.Part.Price.ToString()
                              })
                              .ToArray()));

                e.CreateMap<Supplier, DTOs.Export.SupplierDTO>()
                   .ForMember(x => x.PartsCount, y => y
                       .MapFrom(s => s.Parts.Count));

                e.CreateMap<Sale, DTOs.Export.SaleDTO>()
                    .ForMember(x => x.CustomerName, y => y
                        .MapFrom(s => s.Customer.Name))
                    .ForMember(x => x.Price, y => y
                        .MapFrom(s => s.Car.PartCars
                            .Sum(c => c.Part.Price)))
                    .ForMember(x => x.CarDTO, y => y
                         .MapFrom(s => new DTOs.Export.CarDTO
                         {
                             Make = s.Car.Make,
                             Model = s.Car.Model,
                             TravelledDistance = s.Car.TravelledDistance
                         }));
            });

        public static readonly IMapper Mapper
           = Configuration.CreateMapper();

        public static TDestination MapElement<TSource, TDestination>(TSource from)
        {
            return Mapper.Map<TDestination>(from);
        }

        public static TDestination[] MapCollection<TSource, TDestination>(IEnumerable<TSource> from)
        {
            return from
                .Select(x => Mapper.Map<TDestination>(x))
                .ToArray();
        }
    }
}