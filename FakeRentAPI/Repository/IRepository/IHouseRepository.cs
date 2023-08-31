using FakeRentAPI.Models;
using System.Linq.Expressions;

namespace FakeRentAPI.Repository.IRepository
{
    public interface IHouseRepository : IRepository<House>
    {
        Task<House> UpdateAsync(House house);
    }
}
