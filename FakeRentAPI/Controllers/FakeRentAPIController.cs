using AutoMapper;
using FakeRentAPI.Data;
using FakeRentAPI.Models;
using FakeRentAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FakeRentAPI.Controllers
{
    [Route("api/FakeRentAPI")]
    [ApiController]
    public class FakeRentAPIController : ControllerBase
    {
        private readonly ILogger<FakeRentAPIController> _logger;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public FakeRentAPIController(ILogger<FakeRentAPIController> logger, ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HouseDTO>>> GetHouses()
        {
            IEnumerable<House> houses = await _applicationDbContext.Houses.ToListAsync();
            return Ok(_mapper.Map<List<HouseDTO>>(houses));
        }

        [HttpGet("{id:int}", Name = "GetHouse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HouseDTO>> GetHouse(int id)
        {
            var house = await _applicationDbContext.Houses.FirstOrDefaultAsync(x => x.Id == id);
            if (house != null)
            {
                return Ok(_mapper.Map<HouseDTO>(house));
            }
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HouseDTO>> CreateHouse([FromBody] HouseCreateDTO createDTO)
        {
            if (await _applicationDbContext.Houses.FirstOrDefaultAsync(x => x.Name.ToLower() == createDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "House is alreadys exists");
                return BadRequest(ModelState);
            }
            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }
            House model = _mapper.Map<House>(createDTO);
            await _applicationDbContext.Houses.AddAsync(model);
            await _applicationDbContext.SaveChangesAsync();
            return CreatedAtRoute("GetHouse", new { id = model.Id }, model);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id:int}", Name = "DeleteHouse")]
        public async Task<IActionResult> DeleteHouse(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var house = await _applicationDbContext.Houses.FirstOrDefaultAsync(x => x.Id == id);
            if (house == null)
            {
                return NotFound();
            }

            _applicationDbContext.Houses.Remove(house);
            await _applicationDbContext.SaveChangesAsync();
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id:int}", Name = "UpdateHouse")]
        public async Task<IActionResult> UpdateHouse(int id, [FromBody] HouseUpdateDTO updateDTO)
        {
            if (updateDTO == null || id != updateDTO.Id)
            {
                return BadRequest();
            }

            House model = _mapper.Map<House>(updateDTO);    
            _applicationDbContext.Houses.Update(model);
            await _applicationDbContext.SaveChangesAsync();
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPatch("{id:int}", Name = "UpdatePartialHouse")]
        public async Task<IActionResult> UpdatePartialHouse(int id, JsonPatchDocument<HouseUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var house = await _applicationDbContext.Houses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            HouseUpdateDTO houseDTO = _mapper.Map<HouseUpdateDTO>(house);
            if (house == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(houseDTO, ModelState);

            House model = _mapper.Map<House>(houseDTO);
            _applicationDbContext.Update(model);
            await _applicationDbContext.SaveChangesAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
