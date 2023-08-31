using System.ComponentModel.DataAnnotations;

namespace FakeRent.Web.Models
{
    public class HouseNumberCreateDTO
    {
        [Required]
        public int HouseNo { get; set; }
        [Required]
        public int HouseId { get; set; }
        public string? SpecialDetails { get; set; }
    }
}
