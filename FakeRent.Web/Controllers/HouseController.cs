using AutoMapper;
using FakeRent.Utility;
using FakeRent.Web.Models;
using FakeRent.Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
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
            var response = await _houseService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetails.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<HouseDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateHouse()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateHouse(HouseCreateDTO houseCreateDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _houseService.CreateAsync<APIResponse>(houseCreateDTO, HttpContext.Session.GetString(StaticDetails.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "House created successfully";
                    return RedirectToAction(nameof(IndexHouse));
                }
            }
            return View(houseCreateDTO);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateHouse(int houseId)
        {
            var response = await _houseService.GetAsync<APIResponse>(houseId, HttpContext.Session.GetString(StaticDetails.SessionToken));
            if (response != null && response.IsSuccess)
            {
                HouseDTO model = JsonConvert.DeserializeObject<HouseDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<HouseUpdateDTO>(model));
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateHouse(HouseUpdateDTO houseUpdateDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _houseService.UpdateAsync<APIResponse>(houseUpdateDTO, HttpContext.Session.GetString(StaticDetails.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "House updated successfully";
                    return RedirectToAction(nameof(IndexHouse));
                }
            }
            TempData["error"] = "Error encountered";
            return View(houseUpdateDTO);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteHouse(int houseId)
        {
            var response = await _houseService.GetAsync<APIResponse>(houseId, HttpContext.Session.GetString(StaticDetails.SessionToken));
            if (response != null && response.IsSuccess)
            {
                HouseDTO model = JsonConvert.DeserializeObject<HouseDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteHouse(HouseDTO houseDTO)
        {
            var response = await _houseService.DeleteAsync<APIResponse>(houseDTO.Id, HttpContext.Session.GetString(StaticDetails.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "House deleted successfully";
                return RedirectToAction(nameof(IndexHouse));
            }
            TempData["error"] = "Error encountered";
            return View(houseDTO);
        }
    }
}
