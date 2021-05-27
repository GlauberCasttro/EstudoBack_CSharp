using DEMO_DDD.DOMAIN.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DEMO_DDD.DOMAIN.Interfaces.Services
{
    public interface IClienteService : IServiceBase<Cliente>
    {
       IEnumerable<Cliente> ObterClientesEspeciais(IEnumerable<Cliente> clientes);
        Task<IEnumerable<Cliente>> ObterClientesProdutos();
        Task<IEnumerable<Cliente>> ObterPorNome(string nome, DateTime? date);
    }
}
