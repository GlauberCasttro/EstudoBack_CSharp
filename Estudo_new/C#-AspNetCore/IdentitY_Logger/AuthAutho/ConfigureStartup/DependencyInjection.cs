using AuthAutho.Extensions;
using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAutho.ConfigureStartup
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveDependencias(this IServiceCollection services)
        {
            //ncessario adicionar a injecao de dependencia para a classe criada Permissao necessariaHandler
            //a mesma pode ser singleton devido todo o contexto da aplicacao utilizar a mesma regra
            services.AddSingleton<IAuthorizationHandler, PermissaoNecessariaHandler>();
            services.AddScoped<AuditoriaFilter>();//injeção de dependencia para  o filtro de auditoria
         
            
            //Configuração do KissILogger, IHttpContextAcesso serve para voce pegar em qualquer momento o contexto htttp
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped((context) =>
            {
                return Logger.Factory.Get();
            });

            return services;
        }
    }
}
