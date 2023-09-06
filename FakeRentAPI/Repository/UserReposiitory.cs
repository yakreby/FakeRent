using FakeRentAPI.Data;
using FakeRentAPI.Identity;
using FakeRentAPI.Models;
using FakeRentAPI.Models.Dto;
using FakeRentAPI.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FakeRentAPI.Repository
{
    public class UserReposiitory : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<AppIdentityUser> _userManager;
        private string secretKey;
        public UserReposiitory(ApplicationDbContext applicationDbContext, IConfiguration configuration,
            UserManager<AppIdentityUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }
        public bool IsUniqueUser(string username)
        {
            var user = _applicationDbContext.AppIdentityUsers.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _applicationDbContext.AppIdentityUsers.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDTO.UserName.ToLower());
            //User not found
            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }

            //Checking password of current user
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
            if (!isValid)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }

            //If user found and password is valid, generate JWT Token based on role
            var role = _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            //Generating token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new()
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };
            return loginResponseDTO;
        }

        public async Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            LocalUser user = new()
            {
                UserName = registerationRequestDTO.UserName,
                Password = registerationRequestDTO.Password,
                Name = registerationRequestDTO.Name,
                Role = registerationRequestDTO.Role,
            };

            _applicationDbContext.LocalUsers.Add(user);
            await _applicationDbContext.SaveChangesAsync();
            user.Password = "";
            return user;
        }
    }
}
