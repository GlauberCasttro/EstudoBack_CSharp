using Equipfer.Domain.Interface;
using Equipfer.Domain.Repository;
using Equipfer.Infrastructure.Interface;
using Equipfer.Infrastructure.Services;
using Equipfer.Service.Interface;
using Equipfer.Service.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace EquipferAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Repository
            services.AddTransient<IEquipagemRepository, EquipagemRepository>();
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<IAtividadeRepository, AtividadeRepository>();
            services.AddTransient<ITarefaRepository, TarefaRepository>();
            services.AddTransient<IMacroRepository, MacroRepositoy>();


            // Services
            services.AddTransient<IEquipagemService, EquipagemService>();
            services.AddTransient<ICategoriaService, CategoriaService>();
            services.AddTransient<IAtividadeService, AtividadeService>();
            services.AddTransient<ITarefaService, TarefaService>();
            services.AddTransient<IMacroService, MacroService>();


            // Factory
            services.AddTransient<IOracleCommand, OracleCommandFactory>();
            services.AddSingleton<IOracleConnection, OracleConnectionFactory>();

            // HttpClient
            services.AddHttpClient("CCO_API", client =>
            {
                var ccoUri = Environment.GetEnvironmentVariable("CCO_URI");
                client.BaseAddress = new Uri(ccoUri);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Gestão Eficiente de Equipagem - Equipfer API",
                    Description = "API para disponibilizar informações do legado Equipfer",
                    Version = "1.0",
                    License = new License { Name = "Apache 2.0", Url = "http://www.apache.org/licenses/LICENSE-2.0.html" },
                    Contact = new Contact { Name = "Giovani Felipe Guimarães Santos", Email = "giovani.santos.ext@vli-logistica.com.br" },
                    TermsOfService = "http://swagger.io/terms/"
                });
            });

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Equipfer API - v1");
                c.RoutePrefix = string.Empty;
            });

            loggerFactory.AddFile("Logs/equipfer-api-{Date}.txt");

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
