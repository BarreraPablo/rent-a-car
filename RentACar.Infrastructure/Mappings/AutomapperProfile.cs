using AutoMapper;
using RentACar.Core.CountryDTOs;
using RentACar.Core.DTOs.BodyTypeDTOs;
using RentACar.Core.DTOs.BrandDTOs;
using RentACar.Core.DTOs.CarDTOs;
using RentACar.Core.DTOs.ClientDTOs;
using RentACar.Core.DTOs.DocumentTypeDTOs;
using RentACar.Core.DTOs.PaymentTypeDTOs;
using RentACar.Core.DTOs.ReservationDTOs;
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
            CreateMap<Car, CarReadDto>().ReverseMap();
            CreateMap<CarCreateDto, Car>();
            CreateMap<CarUpdateDto, Car>();
            CreateMap<PaymentType, PaymentTypeReadDto>().ReverseMap();
            CreateMap<PaymentTypeCreateDto, PaymentType>();
            CreateMap<CountryReadDto, Country>().ReverseMap();
            CreateMap<ClientCreateDto, Client>();
            CreateMap<Client, ClientReadDto>();
            CreateMap<ClientUpdateDto, Client>();
            CreateMap<User, UserReadDto>();
            CreateMap<Reservation, ReservationReadDto>();
            CreateMap<ReservationCreateDto, Reservation>();
        }
    }
}
