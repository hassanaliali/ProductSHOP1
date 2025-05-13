using AutoMapper;
using Product.Core.Entities;
using Product.Infrastructure.Data.DTOs;

namespace Product.API.Models
{
    public class MappingProduct:Profile
    {
        public MappingProduct()
        {
            CreateMap<ProductDTO, ProductEntity>().ReverseMap();
            CreateMap<ProductEntity, ProductDTO>()
                .ForMember(d=>d.CategoryName,o=>o.MapFrom(s=>s.category.Name)).ReverseMap();
            CreateMap<ListProductDTO, ProductEntity>().ReverseMap();
            CreateMap<AddProductDTO, ProductEntity>().ReverseMap();
            CreateMap<UpdateProductDTO, ProductEntity>().ReverseMap();
        }
    }
}
