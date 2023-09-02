using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeRentAPI.Models
{
    public class HouseNumber
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HouseNo { get; set; }
        [ForeignKey("House")]
        public int HouseId { get; set; }
        public House? House { get; set; }
        public string? SpecialDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
