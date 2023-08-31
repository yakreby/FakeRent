using FakeRentAPI.Models;

namespace FakeRentAPI.Repository.IRepository
{
    public interface IHouseNumberRepository : IRepository<HouseNumber>
    {
        Task<HouseNumber> UpdateAsync(HouseNumber house);
    }
}
