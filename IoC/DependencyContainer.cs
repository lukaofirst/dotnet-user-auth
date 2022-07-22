using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infraestructure.Data;
using Infraestructure.Repositories;
using Infraestructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public class DependencyContainer
    {
        public static void StartServices(IServiceCollection services)
        {
            // Data Context
            services.AddSingleton<MongoDBContext>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();

            // Services
            services.AddScoped<IJWTManagerService, JWTManagerService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
