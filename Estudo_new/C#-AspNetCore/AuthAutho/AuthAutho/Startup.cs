using AuthAutho.ConfigureStartup;
using AuthAutho.Extensions;
using KissLog;
using KissLog.Apis.v1.Listeners;
using KissLog.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Text;

namespace AuthAutho
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        //mudando a classe startup para rodar com varios ambientes configurados
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;//forma antiga
        //}

        public Startup(IHostEnvironment hostEnvironment)
        {
           
            //Configura��o para trabalhar com os 3 ambientes pr� definidos
            //Dev
            //Staging
            // Production
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();


            //utilizando use secrets para ambiente, utiliza-se quando nao que expor o que est� salvo no secrety
            if (hostEnvironment.IsProduction())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

          

            services.AddControllersWithViews();
            services.AddRazorPages();//necessario para identity 3.1.6 aspnet core 3.1


            //Adicionando configura��o do Identity
            services.AddIdentityConfig(Configuration);


            //Adicionando configura��es de clains e roles do identity
            services.AddAuthorizationConfigIdentity();


            //Adicionando configura��o de inje��o de dependencias
            services.ResolveDependencias();



        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //Tratamento de erro personalizado para erros da aplica��o para ambiente de produ��o
                app.UseExceptionHandler("/erro/500");
                app.UseStatusCodePagesWithRedirects("/erro/{0}");

                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();//necessario para identity 3.1.6 aspnet core 3.1
            app.UseAuthorization();



            //COnfigura��o do KissLogger para logar erros/NameSpace Extensions
            app.UseKissLogMiddleware(options => {
               LoggerConfig.ConfigureKissLog(options, Configuration);
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();//necessario para identity 3.1.6 aspnet core 3.1
            });
        }
    }
}
