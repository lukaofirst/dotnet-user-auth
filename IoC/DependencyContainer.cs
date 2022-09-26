using Application.Interfaces.Services;
using Application.Mappings;
using Application.Services;
using Core.Interfaces.Repositories;
using Infraestructure.Data;
using Infraestructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IoC
{
    public class DependencyContainer
    {
        public static void StartServices(IServiceCollection services, IConfigurationRoot appSettings)
        {
            // Data Context
            services.AddSingleton<MongoDBContext>();

            // AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();

            // Services
            services.AddScoped<IJWTManagerService, JWTManagerService>();
            services.AddScoped<IUserService, UserService>();


            // JWT Authentication
            services.AddAuthentication(x =>
             {
                 x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             }).AddJwtBearer(o =>
             {
                 var key = Encoding.UTF8.GetBytes(appSettings.GetValue<string>("JWTConfig:Key"));
                 o.SaveToken = true;
                 o.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = appSettings.GetValue<string>("JWT:Issuer"),
                     ValidAudience = appSettings.GetValue<string>("JWT:Audience"),
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                 };
             });
        }
    }
}
