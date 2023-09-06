using FakeRentAPI.Identity;
using FakeRentAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FakeRentAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppIdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }

        public DbSet<AppIdentityUser> AppIdentityUsers { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<HouseNumber> HouseNumbers { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<House>().HasData(
                new House
                {
                    Id = 1,
                    Name = "Rock Building",
                    Details = "Something",
                    ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa1.jpg",
                    Occupancy = 4,
                    Rate = 5,
                    SquareFeet = 140,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                },
                new House
                {
                    Id = 2,
                    Name = "Sea House",
                    Details = "Excellent",
                    ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa2.jpg",
                    Occupancy = 12,
                    Rate = 4,
                    SquareFeet = 120,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                },
                new House
                {
                    Id = 3,
                    Name = "Sky House",
                    Details = "Beatiful house",
                    ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa3.jpg",
                    Occupancy = 14,
                    Rate = 2,
                    SquareFeet = 60,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                });
        }
    }
}
