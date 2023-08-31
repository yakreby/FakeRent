using FakeRentAPI.Data;
using FakeRentAPI.Models;
using FakeRentAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FakeRentAPI.Repository
{
    public class HouseRepository : Repository<House>, IHouseRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public HouseRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<House> UpdateAsync(House entity)
        {
            entity.UpdatedDate = DateTime.Now;  
            _applicationDbContext.Houses.Update(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }
    }
}
