using FakeRent.Utility;
using FakeRent.Web.Models;
using FakeRent.Web.Services.IServices;

namespace FakeRent.Web.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string authUrl;
        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration):base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            authUrl = configuration.GetValue<string>("ServiceUrls:FakeRentAPI");
        }
        public Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = loginRequestDTO,
                Url = authUrl + "/api/UserAuth/login"
            });
        }

        public Task<T> RegisterAsync<T>(RegisterationRequestDTO registerationRequestDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = registerationRequestDTO,
                Url = authUrl + "/api/UserAuth/register"
            });
        }
    }
}
