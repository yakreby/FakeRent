using FakeRentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FakeRentAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }
        public DbSet<House> Houses { get; set; }
    }
}
