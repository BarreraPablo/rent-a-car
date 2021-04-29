using AutoMapper;
//using RentACar.Core.DTOs.BrandDTOs;
using RentACar.Core.DTOs.UserDTOs;
using RentACar.Core.Entities;

namespace RentACar.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            //CreateMap<BrandCreateDto, Brand>();
            CreateMap<UserCreateDto, User>().ReverseMap();
        }
    }
}
