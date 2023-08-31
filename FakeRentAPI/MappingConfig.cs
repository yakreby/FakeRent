using AutoMapper;
using FakeRentAPI.Models;
using FakeRentAPI.Models.Dto;

namespace FakeRentAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<House,HouseDTO>();
            CreateMap<HouseDTO, House>();
            CreateMap<HouseNumber, HouseNumberDTO>();
            CreateMap<HouseNumberDTO, HouseNumber>();
            CreateMap<HouseNumber, HouseNumberCreateDTO>().ReverseMap();
            CreateMap<HouseNumber, HouseNumberUpdateDTO>().ReverseMap();
        }
    }
}
