using AutoMapper;
using Bike_Store_App_WebApi.DTO;
using Bike_Store_App_WebApi.Models;

namespace Bike_Store_App_WebApi.MappingProfile
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>()
    .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand != null ? src.Brand.BrandName : null)) // Adjust as per your Brand class
    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : null)); // Adjust as per your Category class


            CreateMap<ProductDTO, Product>();

            CreateMap<Category, CategoryDTO>() ;

            CreateMap<CategoryDTO, Category>();
        }
    }
}
