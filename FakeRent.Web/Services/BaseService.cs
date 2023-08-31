using FakeRent.Web.Models;
using FakeRent.Web.Services.IServices;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using FakeRent.Utility;

namespace FakeRent.Web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse ResponseModel { get; set; }
        //Use for API calls
        public IHttpClientFactory httpClientFactory { get; set; }
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            this.ResponseModel = new APIResponse();
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                //Creating client
                var client = httpClientFactory.CreateClient("FakeRentAPI");
                //Creating Request message
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                //If creating or updating operations will be performed, we need data. (Post and put actions)
                if(apiRequest.Data != null)
                {
                    //Serializing data for passing through Request
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json"); 
                }
                //When sending a request we should assign it's Http type to RequestMessage object's Method property
                switch (apiRequest.ApiType)
                {
                    case StaticDetails.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case StaticDetails.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case StaticDetails.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                //When we send request, we will receive a response back. We will set that to be null by default
                HttpResponseMessage httpResponseMessage = null;
                //Calling the API end point and passing the message that we configured 
                httpResponseMessage = await client.SendAsync(message);

                //When we receive the API response we need to extract the API content
                var apiContent = await httpResponseMessage.Content.ReadAsStringAsync();
                //Deserializing object we get from response content
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;
            }
            catch(Exception ex)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                //We cannot return dto directly, it must be the type T we already defined before
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}
