using AutoMapper;
using System.Linq;
using ProductShop.DTOs;
using ProductShop.Models;
using System.Collections.Generic;

namespace ProductShop.MapperConfigs
{
    public static class MapperApplier
    {
        public static readonly IConfigurationProvider MapperConfiguration
            = new MapperConfiguration(m =>
            {
                m.CreateMap<Task1UserDTO, User>();
                m.CreateMap<Task2ProductDTO, Product>();
                m.CreateMap<Task3CategoryDTO, Category>();
                m.CreateMap<Task4CategoryProductDTO, CategoryProduct>();

                m.CreateMap<Product, Task5ProductDTO>()
                    .ForMember(x => x.Buyer, y => y
                           .MapFrom(s => s.Buyer.FirstName + " " + s.Buyer.LastName));

                m.CreateMap<Product, Task6ProductDTO>();

                m.CreateMap<User, Task6UserDTO>()
                    .ForMember(x => x.SoldProducts, y => y
                        .MapFrom(s => s.ProductsSold));

                m.CreateMap<Category, Task7CategoryDTO>()
                    .ForMember(x => x.Count, y => y
                          .MapFrom(s => s.CategoryProducts.Count))
                    .ForMember(x => x.TotalRevenue, y => y
                          .MapFrom(s => s.CategoryProducts.Sum(x => x.Product.Price)))
                    .ForMember(x => x.AveragePrice, y => y
                        .MapFrom(s => s.CategoryProducts.Average(x => x.Product.Price)));

                m.CreateMap<Task8UserDTO[], Task8UsersDTO>()
                    .ForMember(x => x.Count, y => y
                        .MapFrom(s => s.Sum(x => x.SoldProducts.Count)))
                    .ForMember(x => x.users, y => y
                        .MapFrom(s => s));
            });

        public static readonly IMapper MapperInst
            = MapperConfiguration.CreateMapper();

        public static TDestination MapElement<TSource, TDestination>(TSource from)
            => MapperInst.Map<TDestination>(from);

        public static ICollection<TDestination> MapCollection<TSource, TDestination>(ICollection<TSource> from)
            => from.Select(x => MapElement<TSource, TDestination>(x)).ToList();
    }
}
