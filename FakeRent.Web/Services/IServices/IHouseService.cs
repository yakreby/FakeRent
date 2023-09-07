using FakeRent.Web.Models;

namespace FakeRent.Web.Services.IServices
{
    public interface IHouseService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(HouseCreateDTO houseCreateDTO, string token);
        Task<T> UpdateAsync<T>(HouseUpdateDTO houseUpdateDTO, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
