using AutoMapper;
using FakeRentAPI.Identity;
using FakeRentAPI.Models;
using FakeRentAPI.Models.Dto;

namespace FakeRentAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<House, HouseDTO>();
            CreateMap<HouseDTO, House>();
            CreateMap<House, HouseCreateDTO>().ReverseMap();
            CreateMap<House, HouseUpdateDTO>().ReverseMap();

            CreateMap<HouseNumber, HouseNumberDTO>().ReverseMap();
            CreateMap<HouseNumber, HouseNumberCreateDTO>().ReverseMap();
            CreateMap<HouseNumber, HouseNumberUpdateDTO>().ReverseMap();
            CreateMap<AppIdentityUser, UserDTO>().ReverseMap();
        }
    }
}
