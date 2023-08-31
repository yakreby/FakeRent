using System.ComponentModel.DataAnnotations;

namespace FakeRentAPI.Models.Dto
{
    public class HouseNumberCreateDTO
    {
        [Required]
        public int HouseNo { get; set; }
        public string? SpecialDetails { get; set; }
    }
}
