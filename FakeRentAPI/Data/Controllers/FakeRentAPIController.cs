using FakeRentAPI.Data;
using FakeRentAPI.Models;
using FakeRentAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FakeRentAPI.Data.Controllers
{
    [Route("api/FakeRentAPI")]
    [ApiController]
    public class FakeRentAPIController : ControllerBase
    {
        private readonly ILogger<FakeRentAPIController> _logger;
        public FakeRentAPIController(ILogger<FakeRentAPIController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<HouseDTO>> GetHouses()
        {
            _logger.LogInformation("Getting all houses");
            return Ok(HouseStore.HouseList);
        }

        [HttpGet("{id:int}", Name = "GetHouse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<HouseDTO> GetHouse(int id)
        {
            HouseDTO house = HouseStore.HouseList.Where(x => x.Id == id).FirstOrDefault();
            if (house != null)
            {
                return Ok(house);
            }
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<HouseDTO> CreateHouse([FromBody] HouseDTO houseDTO)
        {
            if (HouseStore.HouseList.FirstOrDefault(x => x.Name.ToLower() == houseDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "House is alreadys exists");
                return BadRequest(ModelState);
            }
            if (houseDTO == null)
            {
                return BadRequest(houseDTO);
            }
            //This is not a create request
            if (houseDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            houseDTO.Id = HouseStore.HouseList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            HouseStore.HouseList.Add(houseDTO);
            return CreatedAtRoute("GetHouse", new { id = houseDTO.Id }, houseDTO);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id:int}", Name = "DeleteHouse")]
        public IActionResult DeleteHouse(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            HouseDTO houseDTO = HouseStore.HouseList.FirstOrDefault(x => x.Id == id);
            if (houseDTO == null)
            {
                return NotFound();
            }

            HouseStore.HouseList.Remove(houseDTO);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id:int}", Name = "UpdateHouse")]
        public IActionResult UpdateHouse(int id, [FromBody] HouseDTO houseDTO)
        {
            if (houseDTO == null || id != houseDTO.Id)
            {
                return BadRequest();
            }

            var house = HouseStore.HouseList.FirstOrDefault(x => x.Id == id);
            house.Name = houseDTO.Name;
            house.Occupancy = houseDTO.Occupancy;
            house.SquareFeet = houseDTO.SquareFeet;

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPatch("{id:int}", Name = "UpdatePartialHouse")]
        public IActionResult UpdatePartialHouse(int id, JsonPatchDocument<HouseDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var house = HouseStore.HouseList.FirstOrDefault(x => x.Id == id);
            if (house == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(house, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
