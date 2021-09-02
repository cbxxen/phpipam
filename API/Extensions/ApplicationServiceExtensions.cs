/*
    Class used to startup the Services in Startup.cs
    It helps to make Startup.cs more Slim
*/
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using API.Helper;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config){
            //saving configuration in CloudinarySettings helper. Looks up values in appsettings.json (CloudinarySettings)
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            //service to store and delete files in cloudinary
            services.AddScoped<IPhotoService, PhotoService>();
            //Token Service AddScoped=after the service is used, it gets suspended again
            services.AddScoped<ITokenService, TokenService>();
            //Service for Repository
            services.AddScoped<IUserRepository, UserRepository>();
            //Auto Mapper
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            //SQL Connection
            services.AddDbContext<DataContext>(options =>
            {
                //the options are saved in appsettings.Development.json and written to _config
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}