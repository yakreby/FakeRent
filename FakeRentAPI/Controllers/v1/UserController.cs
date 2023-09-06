using FakeRentAPI.Models;
using FakeRentAPI.Models.Dto;
using FakeRentAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FakeRentAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/UserAuth")]
    [ApiController]
    [ApiVersionNeutral]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        protected APIResponse _response;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _response = new();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var loginResponse = await _userRepository.Login(loginRequestDTO);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or password is incorrect");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO registerationRequestDTO)
        {
            bool IsUserUnique = _userRepository.IsUniqueUser(registerationRequestDTO.UserName);
            if (IsUserUnique == false)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username already exists");
                return BadRequest(_response);
            }

            var user = _userRepository.Register(registerationRequestDTO);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Error while registering");
                return BadRequest(_response);
            }
            _userRepository.Register(registerationRequestDTO);
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;

            return Ok(_response);
        }
    }
}
