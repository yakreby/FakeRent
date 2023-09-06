using FakeRentAPI.Data;
using FakeRentAPI.Repository;
using FakeRentAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FakeRentAPI.Identity;

namespace FakeRentAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers(options =>
            {
                options.CacheProfiles.Add("Default30",
                    new CacheProfile()
                    {
                        Duration = 30
                    });
            }).AddNewtonsoftJson();
            builder.Services.AddEndpointsApiExplorer();

            //Swagger Configuration
            builder.Services.AddSwaggerGen(options =>
            {
                //How the API is protected through the generated Swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
                        "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                        "Example: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
                options.SwaggerDoc("v1",new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "Fake Rent v1",
                    Description = "Api to Rent Houses",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Xlrware",
                        Url = new Uri("https://xlrware.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "License of App",
                        Url = new Uri("https://xlrware.com")
                    }
                });
                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2.0",
                    Title = "Fake Rent v2",
                    Description = "Api to Rent Houses",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Xlrware",
                        Url = new Uri("https://xlrware.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "License of App",
                        Url = new Uri("https://xlrware.com")
                    }
                });
            });

            //Identity
            builder.Services.AddIdentity<AppIdentityUser, AppIdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //Caching
            builder.Services.AddMemoryCache();

            //Versioning
            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });
            builder.Services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            //Repository injection
            builder.Services.AddScoped<IHouseRepository, HouseRepository>();
            builder.Services.AddScoped<IHouseNumberRepository, HouseNumberRepository>();
            builder.Services.AddScoped<IUserRepository, UserReposiitory>();

            //Retrieve secret key
            var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");
            //Auth
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

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
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "FakeRentV1");
                    options.SwaggerEndpoint("/swagger/v2/swagger.json", "FakeRentV2");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}