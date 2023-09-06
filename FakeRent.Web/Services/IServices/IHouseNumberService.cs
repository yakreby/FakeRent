using FakeRent.Web.Models;

namespace FakeRent.Web.Services.IServices
{
    public interface IHouseNumberService
    {
        Task<T> GetAllAsync<T>(string token); 
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(HouseNumberCreateDTO houseNumberCreateDTO, string token);
        Task<T> UpdateAsync<T>(HouseNumberUpdateDTO houseNumberUpdateDTO, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
