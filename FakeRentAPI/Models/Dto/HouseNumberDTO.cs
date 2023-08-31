using System.ComponentModel.DataAnnotations;

namespace FakeRentAPI.Models.Dto
{
    public class HouseNumberDTO
    {
        [Required]
        public int HouseNo { get; set; }
        public string? SpecialDetails { get; set; }
    }
}
