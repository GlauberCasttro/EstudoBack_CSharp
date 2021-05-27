using AuthAutho.Areas.Identity.Data;
using AuthAutho.Data;
using AuthAutho.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAutho.ConfigureStartup
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddAuthorizationConfigIdentity(this IServiceCollection services)
        {
            //Adicionando configuração para utilização de clains  padrão que a microsift recomenda
            services.AddAuthorization(options =>
            {
                options.AddPolicy("PodeExcluir", policy => policy.RequireClaim("PodeExcluir"));
                options.AddPolicy("PodeLer", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeLer")));
                options.AddPolicy("PodeEscrever", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeEscrever")));
            });
            return services;
        }

        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            //Configurando o identity
            services.AddDbContext<AuthDbContext>(options =>
            options.UseSqlServer(
            configuration.GetConnectionString("AuthDbContextConnection")));
            services.AddDefaultIdentity<ApplicationUser>(options =>
            options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()//Necessario adicionar para trabalhar com roles
                .AddEntityFrameworkStores<AuthDbContext>();

            return services;
        }
    }

}
