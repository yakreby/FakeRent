using FakeRentAPI.Data;
using FakeRentAPI.Repository;
using FakeRentAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FakeRentAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Repository injection
            builder.Services.AddScoped<IHouseRepository, HouseRepository>();
            builder.Services.AddScoped<IHouseNumberRepository, HouseNumberRepository>();

            //Mapping
            builder.Services.AddAutoMapper(typeof(MappingConfig));

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }); 
            //Logging
            //Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
            //    .WriteTo.File("log / logs.txt", rollingInterval: RollingInterval.Day).CreateLogger();
            //builder.Host.UseSerilog();
            
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}