using AutoMapper;
using FakeRentAPI.Models;
using FakeRentAPI.Models.Dto;
using FakeRentAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FakeRentAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/FakeRentAPI")]
    [ApiController]
    public class FakeRentAPIController : ControllerBase
    {
        private readonly ILogger<FakeRentAPIController> _logger;
        private readonly IHouseRepository _repository;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public FakeRentAPIController(ILogger<FakeRentAPIController> logger, IHouseRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _response = new();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetHouses()
        {
            try
            {
                IEnumerable<House> houses = await _repository.GetAllAsync();
                _response.Result = _mapper.Map<List<HouseDTO>>(houses);
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

        [HttpGet("{id:int}", Name = "GetHouse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetHouse(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var house = await _repository.GetAsync(x => x.Id == id);
                if (house == null)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound();
                }
                _response.Result = _mapper.Map<HouseDTO>(house);
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

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> CreateHouse([FromBody] HouseCreateDTO createDTO)
        {
            try
            {
                if (await _repository.GetAsync(x => x.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "House is alreadys exists");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }
                House house = _mapper.Map<House>(createDTO);
                await _repository.CreateAsync(house);

                _response.Result = _mapper.Map<HouseDTO>(house);
                _response.StatusCode = System.Net.HttpStatusCode.Created;
                return CreatedAtRoute("GetHouse", new { id = house.Id }, _response);
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

        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id:int}", Name = "DeleteHouse")]
        public async Task<ActionResult<APIResponse>> DeleteHouse(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var house = await _repository.GetAsync(x => x.Id == id);
                if (house == null)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound();
                }

                await _repository.RemoveAsync(house);
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

        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id:int}", Name = "UpdateHouse")]
        public async Task<ActionResult<APIResponse>> UpdateHouse(int id, [FromBody] HouseUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                House model = _mapper.Map<House>(updateDTO);
                await _repository.UpdateAsync(model);
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

        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPatch("{id:int}", Name = "UpdatePartialHouse")]
        public async Task<IActionResult> UpdatePartialHouse(int id, JsonPatchDocument<HouseUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var house = await _repository.GetAsync(x => x.Id == id, tracked: false);
            HouseUpdateDTO houseDTO = _mapper.Map<HouseUpdateDTO>(house);
            if (house == null)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            patchDTO.ApplyTo(houseDTO, ModelState);

            House model = _mapper.Map<House>(houseDTO);
            await _repository.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
