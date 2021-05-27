using System;
using AutoMapper;
using DEMO_DDD.APPLICATION.AutoMapper;
using DEMO_DDD.APPLICATION.Interfaces;
using DEMO_DDD.APPLICATION.Services;
using DEMO_DDD.DOMAIN.Interfaces.Repositories;
using DEMO_DDD.DOMAIN.Interfaces.Services;
using DEMO_DDD.DOMAIN.Services;
using DEMO_DDD.INFRA.DATA.Contexto;
using DEMO_DDD.INFRA.DATA.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IOC
{
    public static class DependenciesInjectionConfig
    {
        public static IServiceCollection ResolveDependencias(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<DemoDDDContext>();
            services.AddDbContext<DemoDDDContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
           
            services.AddMemoryCache();
     
            return services;
        }
    }
}
