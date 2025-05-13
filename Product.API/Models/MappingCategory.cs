using AutoMapper;
using Product.Core.Entities;
using Product.Infrastructure.Data.DTOs;

namespace Product.API.Models
{
    public class MappingCategory:Profile
    {
        public MappingCategory()
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<ListCategoryDTO, Category>().ReverseMap();
        }
    }
}
