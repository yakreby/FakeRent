using AutoMapper;
using FakeRent.Utility;
using FakeRent.Web.Models;
using FakeRent.Web.Models.ViewModels;
using FakeRent.Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

namespace FakeRent.Web.Controllers
{
    public class HouseNumberController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IHouseNumberService _houseNumberService;
        private readonly IHouseService _houseService;
        public HouseNumberController(IHouseNumberService houseNumberService, IHouseService houseService, IMapper mapper)
        {
            _houseNumberService = houseNumberService;
            _mapper = mapper;
            _houseService = houseService;
        }

        public async Task<IActionResult> IndexHouseNumber()
        {
            List<HouseNumberDTO> list = new();
            var response = await _houseNumberService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetails.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<HouseNumberDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateHouseNumber()
        {
            HouseNumberCreateViewModel createViewModel = new();
			var response = await _houseService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetails.SessionToken));
			if (response != null && response.IsSuccess)
			{
				createViewModel.HouseList = JsonConvert.DeserializeObject<List<HouseDTO>>
                    (Convert.ToString(response.Result)).Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    });
			}
			return View(createViewModel);   
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateHouseNumber(HouseNumberCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _houseNumberService.CreateAsync<APIResponse>(model.HouseNumber, HttpContext.Session.GetString(StaticDetails.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexHouseNumber));
                }
                else
                {
                    if(response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var resp = await _houseService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetails.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.HouseList = JsonConvert.DeserializeObject<List<HouseDTO>>
                    (Convert.ToString(resp.Result)).Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    });
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateHouseNumber(int houseNo)
        {
            HouseNumberUpdateViewModel updateViewModel = new();
            var response = await _houseNumberService.GetAsync<APIResponse>(houseNo, HttpContext.Session.GetString(StaticDetails.SessionToken));
            if (response != null && response.IsSuccess)
            {
                HouseNumberDTO model = JsonConvert.DeserializeObject<HouseNumberDTO>(Convert.ToString(response.Result));
                updateViewModel.HouseNumber = _mapper.Map<HouseNumberUpdateDTO>(model);
            }
            response = await _houseService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetails.SessionToken));
            if (response != null && response.IsSuccess)
            {
                updateViewModel.HouseList = JsonConvert.DeserializeObject<List<HouseDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(updateViewModel);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateHouseNumber(HouseNumberUpdateViewModel updateViewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _houseNumberService.UpdateAsync<APIResponse>(updateViewModel.HouseNumber, HttpContext.Session.GetString(StaticDetails.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexHouseNumber));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var resp = await _houseService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetails.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                updateViewModel.HouseList = JsonConvert.DeserializeObject<List<HouseDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            return View(updateViewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteHouseNumber(int houseNo)
        {
            HouseNumberDeleteViewModel deleteViewModel = new();
            var response = await _houseNumberService.GetAsync<APIResponse>(houseNo, HttpContext.Session.GetString(StaticDetails.SessionToken));
            if (response != null && response.IsSuccess)
            {
                HouseNumberDTO model = JsonConvert.DeserializeObject<HouseNumberDTO>(Convert.ToString(response.Result));
                deleteViewModel.HouseNumber = model;
            }
            response = await _houseService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetails.SessionToken));
            if (response != null && response.IsSuccess)
            {
                deleteViewModel.HouseList = JsonConvert.DeserializeObject<List<HouseDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(deleteViewModel);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteHouseNumber(HouseNumberDeleteViewModel deleteViewModel)
        {
            var response = await _houseNumberService.DeleteAsync<APIResponse>(deleteViewModel.HouseNumber.HouseNo, HttpContext.Session.GetString(StaticDetails.SessionToken));
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexHouseNumber));
            }
            return View(deleteViewModel);
        }
    }
}
