using Autofac;
using DEMO_DDD.APPLICATION.Interfaces;
using DEMO_DDD.APPLICATION.Services;
using DEMO_DDD.APPLICATION.ViewModels;
using DEMO_DDD.DOMAIN.Entidades;
using DEMO_DDD.DOMAIN.Interfaces.Repositories;
using DEMO_DDD.DOMAIN.Interfaces.Services;
using DEMO_DDD.DOMAIN.Services;
using DEMO_DDD.INFRA.DATA.Repositories;

namespace IOC
{
    public class ConfigurationIOC
    {
        public static void Load(ContainerBuilder builder)
        {
            #region IOC DI

            //Camada Application
            builder.RegisterType<AppClienteService>().As<IClienteAppService>();
            builder.RegisterType<AppProdutoService>().As<IProdutoAppService>();


            //Camada de Dominio
            builder.RegisterType<ClienteService>().As<IClienteService>();
            builder.RegisterType<ProdutoService>().As<IProdutoService>();

            //Camada de Repositotorio
            builder.RegisterType<ClienteRepository>().As<IClienteRepository>();
            builder.RegisterType<ProdutoRepository>().As<IProdutoRepository>();
            #endregion
        }
    }
}
