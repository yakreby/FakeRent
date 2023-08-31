using AutoMapper;
using FakeRent.Web.Models;
using FakeRent.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FakeRent.Web.Controllers
{
    public class HouseController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IHouseService _houseService;
        public HouseController(IHouseService houseService, IMapper mapper)
        {
            _houseService = houseService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexHouse()
        {
            List<HouseDTO> list = new();
            var response = await _houseService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<HouseDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> CreateHouse()
        {

            return View();
        }

        public async Task<IActionResult> UpdateHouse()
        {

            return View();
        }

        public async Task<IActionResult> DeleteHouse(int id)
        {

            return View();
        }
    }
}
