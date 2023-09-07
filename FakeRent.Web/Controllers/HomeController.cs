using AutoMapper;
using FakeRent.Utility;
using FakeRent.Web.Models;
using FakeRent.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FakeRent.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHouseService _houseService;
        private readonly IMapper _mapper;
        public HomeController(IHouseService houseService, IMapper mapper)
        {
            _houseService = houseService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<HouseDTO> list = new();

            var response = await _houseService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetails.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<HouseDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}