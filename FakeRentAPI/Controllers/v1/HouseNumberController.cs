using AutoMapper;
using FakeRentAPI.Models.Dto;
using FakeRentAPI.Models;
using FakeRentAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using FakeRent.Utility;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace FakeRentAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/HouseNumber")]
    [ApiVersion("1.0")]
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

        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetHouseNumbers(int pageSize = 3, int pageNumber = 1)
        {
            try
            {
                IEnumerable<HouseNumber> houseNumbers = await _houseNumberRepository.GetAllAsync(includeProperties: "House",
                    pageSize:pageSize, pageNumber:pageNumber);
                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<HouseNumberDTO>>(houseNumbers);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>
                {
                    ex.ToString()
                };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "CreateHouseNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetHouseNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var house = await _houseNumberRepository.GetAsync(x => x.HouseNo == id);
                if (house == null)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound();
                }
                _response.Result = _mapper.Map<HouseNumberDTO>(house);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>
                {
                    ex.ToString()
                };
            }
            return _response;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateHouseNumber([FromBody] HouseNumberCreateDTO createDTO)
        {
            try
            {
                if (await _houseNumberRepository.GetAsync(x => x.HouseNo == createDTO.HouseNo) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "House is alreadys exists");
                    return BadRequest(ModelState);
                }
                if (await _houseRepository.GetAsync(x => x.Id == createDTO.HouseId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "House Id is invalid");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                HouseNumber houseNumber = _mapper.Map<HouseNumber>(createDTO);
                await _houseNumberRepository.CreateAsync(houseNumber);

                _response.Result = _mapper.Map<HouseNumberDTO>(houseNumber);
                _response.StatusCode = System.Net.HttpStatusCode.Created;
                return CreatedAtRoute("GetHouse", new { id = houseNumber.HouseNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>
                {
                    ex.ToString()
                };
            }
            return _response;
        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id:int}", Name = "DeleteHouseNumber")]
        public async Task<ActionResult<APIResponse>> DeleteHouseNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var house = await _houseNumberRepository.GetAsync(x => x.HouseNo == id);
                if (house == null)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound();
                }

                await _houseNumberRepository.RemoveAsync(house);
                _response.StatusCode = System.Net.HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>
                {
                    ex.ToString()
                };
            }
            return _response;
        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id:int}", Name = "UpdateHouseNumber")]
        public async Task<ActionResult<APIResponse>> UpdateHouseNumber(int id, [FromBody] HouseNumberUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if (await _houseRepository.GetAsync(x => x.Id == updateDTO.HouseId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "House Id is invalid");
                    return BadRequest(ModelState);
                }
                HouseNumber model = _mapper.Map<HouseNumber>(updateDTO);
                await _houseNumberRepository.UpdateAsync(model);
                _response.StatusCode = System.Net.HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>
                {
                    ex.ToString()
                };
            }
            return _response;
        }

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPatch("{id:int}", Name = "UpdatePartialHouseNumber")]
        public async Task<IActionResult> UpdatePartialHouseNumber(int id, JsonPatchDocument<HouseNumberUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var house = await _houseNumberRepository.GetAsync(x => x.HouseNo == id, tracked: false);
            HouseNumberUpdateDTO houseDTO = _mapper.Map<HouseNumberUpdateDTO>(house);
            if (house == null)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            patchDTO.ApplyTo(houseDTO, ModelState);

            HouseNumber model = _mapper.Map<HouseNumber>(houseDTO);
            await _houseNumberRepository.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
