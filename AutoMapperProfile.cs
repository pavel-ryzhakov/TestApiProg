using AutoMapper;
using TestApiProg.Dtos;
using TestApiProg.Models;


namespace TestApiProg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, GetProductDto>();
            CreateMap<AddProductDto, Product>();
        }
    }
}
