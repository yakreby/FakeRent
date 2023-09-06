using AutoMapper;
using FakeRentAPI.Models.Dto;
using FakeRentAPI.Models;
using FakeRentAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using FakeRent.Utility;
using Microsoft.AspNetCore.Authorization;
using FakeRentAPI.Controllers.v1;

namespace FakeRentAPI.Controllers.v2
{
    [Route("api/v{version:apiVersion}/HouseNumber")]
    [ApiVersion("2.0")]
    [ApiController]
    public class HouseNumberController : Controller
    {
        private readonly ILogger<FakeRentAPIController> _logger;
        private readonly IHouseNumberRepository _houseNumberRepository;
        private readonly IHouseRepository _houseRepository;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public HouseNumberController(ILogger<FakeRentAPIController> logger, IHouseNumberRepository houseNumberRepository, IMapper mapper, IHouseRepository houseRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _houseNumberRepository = houseNumberRepository;
            _response = new();
            _houseRepository = houseRepository;
        }

        [HttpGet("GetString")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
