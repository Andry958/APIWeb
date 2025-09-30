using AutoMapper;
using BusinessLogic.DTOs.CategoryDTO;
using BusinessLogic.DTOs.ResourceDTO;
using DataAccess.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Configurations
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //CreateMap<CreateProductDto, Product>();
            //CreateMap<EditProductDto, Product>();
            //CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<Resource, ResourceGetDTO>().ReverseMap();
            CreateMap<Resource, ResourceCreateDTO>();
            CreateMap<Resource, ResourceEditDTO>();

            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryCreateDTO>().ReverseMap();
        }
    }
}
