using Microsoft.AspNetCore.Identity;

namespace FakeRentAPI.Identity
{
    public class AppIdentityUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? City { get; set; }
    }
}
