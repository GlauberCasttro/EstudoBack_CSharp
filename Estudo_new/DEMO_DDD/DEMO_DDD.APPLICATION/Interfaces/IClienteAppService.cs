using DEMO_DDD.APPLICATION.ViewModels;
using DEMO_DDD.DOMAIN.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DEMO_DDD.APPLICATION.Interfaces
{
    public interface IClienteAppService : IAppServiceBase<Cliente>
    {
        Task<IEnumerable<Cliente>> ObterClientesEspeciaisAsync();
        Task<IEnumerable<Cliente>> ObterClientesProdutos();
        Task<IEnumerable<Cliente>> ObterPorNome(string nome, DateTime? date);
    }
}
