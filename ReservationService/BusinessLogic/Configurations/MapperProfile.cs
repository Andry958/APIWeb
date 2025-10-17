using AutoMapper;
using BusinessLogic.DTOs.CategoryDTO;
using BusinessLogic.DTOs.ResourceDTO;
using BusinessLogic.DTOs.Users;
using DataAccess.Data.Entities;

namespace BusinessLogic.Configurations
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Resource, ResourceGetDTO>().ReverseMap();
            CreateMap<ResourceCreateDTO, Resource>();
            CreateMap<ResourceEditDTO, Resource>();

            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryCreateDTO>().ReverseMap();

            CreateMap<UserRegisterModel, User>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(model => model.Login))
                .ForMember(x => x.PasswordHash, opt => opt.Ignore());
            CreateMap<User, UserGetModel>();
        }
    }
}
