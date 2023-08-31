using FakeRent.Web.Models;

namespace FakeRent.Web.Services.IServices
{
    public interface IHouseService
    {
        Task<T> GetAllAsync<T>(); 
        Task<T> GetAsync<T>(string id);
        Task<T> CreateAsync<T>(HouseCreateDTO houseCreateDTO);
        Task<T> UpdateAsync<T>(HouseUpdateDTO houseUpdateDTO);
        Task<T> DeleteAsync<T>(int id);
    }
}
