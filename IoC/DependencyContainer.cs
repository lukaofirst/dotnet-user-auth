﻿using Infraestructure.Data;
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

            // Services
        }
    }
}
