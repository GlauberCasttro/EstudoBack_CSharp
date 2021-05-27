using DEMO_DDD.DOMAIN.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DEMO_DDD.DOMAIN.Interfaces.Repositories
{
    public interface IClienteRepository : IRepositoryBase<Cliente>
    {
        Task<Produto> ObterProdutosPorCliente(int id);
        Task<IEnumerable<Cliente>> ObterClientesProdutos();

        Task<IEnumerable<Cliente>> ObterPorNome(string nome, DateTime? date);
    }
}
