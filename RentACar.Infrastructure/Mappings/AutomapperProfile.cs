using AutoMapper;
using RentACar.Core.DTOs.BodyTypeDTOs;
using RentACar.Core.DTOs.BrandDTOs;
using RentACar.Core.DTOs.DocumentTypeDTOs;
//using RentACar.Core.DTOs.BrandDTOs;
using RentACar.Core.DTOs.UserDTOs;
using RentACar.Core.Entities;

namespace RentACar.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Brand, BrandReadDto>().ReverseMap();
            CreateMap<BrandCreateDto, Brand>();
            CreateMap<UserCreateDto, User>().ReverseMap();
            CreateMap<BodyTypeCreateDto, BodyType>().ReverseMap();
            CreateMap<BodyTypeReadDto, BodyType>().ReverseMap();
            CreateMap<DocumentTypeCreateDto, DocumentType>().ReverseMap();
            CreateMap<DocumentTypeReadDto, DocumentType>().ReverseMap();
        }
    }
}
