using FakeRent.Web.Models;

namespace FakeRent.Web.Services.IServices
{
    public interface IHouseNumberService
    {
        Task<T> GetAllAsync<T>(); 
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(HouseNumberCreateDTO houseNumberCreateDTO);
        Task<T> UpdateAsync<T>(HouseNumberUpdateDTO houseNumberUpdateDTO);
        Task<T> DeleteAsync<T>(int id);
    }
}
