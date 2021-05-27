using AutoMapper;
using DEMO_DDD.APPLICATION.ViewModels;
using DEMO_DDD.DOMAIN.Entidades;

namespace DEMO_DDD.APPLICATION.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        //Criando o mapeamento das classes de dominio para as classes de apresentação.
        public AutoMapperConfig()
        {
            CreateMap<Cliente, ClienteViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        }



    }
}
