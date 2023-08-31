using AutoMapper;
using FakeRent.Web.Models;

namespace FakeRent.Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<HouseDTO, HouseCreateDTO>().ReverseMap();
            CreateMap<HouseDTO, HouseUpdateDTO>().ReverseMap();
            CreateMap<HouseNumberDTO, HouseNumberCreateDTO>().ReverseMap();
            CreateMap<HouseNumberDTO, HouseNumberUpdateDTO>().ReverseMap();
        }
    }
}
