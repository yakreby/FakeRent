using FakeRent.Web.Models.Dto;

namespace FakeRent.Web.Models
{
    public class LoginResponseDTO
    {
        public UserDTO? User { get; set; }
        public string? Token { get; set; }
    }
}
