using FakeRentAPI.Models;

namespace FakeRentAPI.Repository.IRepository
{
    public interface IHouseRepository : IRepository<House>
    {
        Task<House> UpdateAsync(House house);
    }
}
