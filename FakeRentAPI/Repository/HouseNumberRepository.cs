using FakeRentAPI.Data;
using FakeRentAPI.Models;
using FakeRentAPI.Repository.IRepository;

namespace FakeRentAPI.Repository
{
    public class HouseNumberRepository : Repository<HouseNumber>, IHouseNumberRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public HouseNumberRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<HouseNumber> UpdateAsync(HouseNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _applicationDbContext.HouseNumbers.Update(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }
    }
}
