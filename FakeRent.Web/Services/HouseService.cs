using FakeRent.Utility;
using FakeRent.Web.Models;
using FakeRent.Web.Services.IServices;

namespace FakeRent.Web.Services
{
    //Base service make possible to call the API, so we implement that
    public class HouseService : BaseService, IHouseService
    {
        private readonly string apiURl;
        private readonly IHttpClientFactory _httpClientFactory;
        //Base service requires HttpClientFactory, that why we use :base statement
        public HouseService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            //Getting url from appsettings.json
            apiURl = configuration.GetValue<string>("ServiceUrls:FakeRentAPI");

        }
        public Task<T> CreateAsync<T>(HouseCreateDTO houseCreateDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = houseCreateDTO,
                Url = apiURl + "/api/FakeRentAPI"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetails.ApiType.DELETE,
                Url = apiURl + "/api/FakeRentAPI" + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = apiURl + "/api/FakeRentAPI"
            });
        }

        public Task<T> GetAsync<T>(string id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = apiURl + "/api/FakeRentAPI" + id
            });
        }

        public Task<T> UpdateAsync<T>(HouseUpdateDTO houseUpdateDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticDetails.ApiType.PUT,
                Data = houseUpdateDTO,
                //Id in here is optional
                Url = apiURl + "/api/FakeRentAPI/"+houseUpdateDTO.Id
            });
        }
    }
}
