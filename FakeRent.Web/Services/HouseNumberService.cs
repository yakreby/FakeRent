using FakeRent.Utility;
using FakeRent.Web.Models;
using FakeRent.Web.Services.IServices;

namespace FakeRent.Web.Services
{
    public class HouseNumberService : BaseService, IHouseNumberService
    {
        private readonly string apiURl;
        private readonly IHttpClientFactory _httpClientFactory;
        //Base service requires HttpClientFactory, that why we use :base statement
        public HouseNumberService(IHttpClientFactory httpClientFactory, IConfiguration configuration):base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            //Getting url from appsettings.json
            apiURl = configuration.GetValue<string>("ServiceUrls:FakeRentAPI");

        }
        public Task<T> CreateAsync<T>(HouseNumberCreateDTO houseNumberCreateDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = houseNumberCreateDTO,
                Url = apiURl + "/api/HouseNumber/"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetails.ApiType.DELETE,
                Url = apiURl + "/api/HouseNumber/" + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = apiURl + "/api/HouseNumber/"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = apiURl + "/api/HouseNumber/" + id
            });
        }

        public Task<T> UpdateAsync<T>(HouseNumberUpdateDTO houseNumberUpdateDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetails.ApiType.PUT,
                Data = houseNumberUpdateDTO,
                //Id in here is optional
                Url = apiURl + "/api/HouseNumber/" + houseNumberUpdateDTO.HouseNo
            });
        }
    }
}
