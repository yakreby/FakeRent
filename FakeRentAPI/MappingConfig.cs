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
            CreateMap<House, HouseCreateDTO>().ReverseMap();
            CreateMap<House, HouseUpdateDTO>().ReverseMap();
        }
    }
}
