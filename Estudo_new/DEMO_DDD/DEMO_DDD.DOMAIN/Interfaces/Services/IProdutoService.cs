using DEMO_DDD.DOMAIN.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DEMO_DDD.DOMAIN.Interfaces.Services
{
    public interface IProdutoService : IServiceBase<Produto>
    {
        Task<IEnumerable<Produto>> BuscarPorNome(string nome);
    }
    
}
