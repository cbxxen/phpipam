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

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config){
             //Token Service AddScoped=after the service is used, it gets suspended again
            services.AddScoped<ITokenService, TokenService>();
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