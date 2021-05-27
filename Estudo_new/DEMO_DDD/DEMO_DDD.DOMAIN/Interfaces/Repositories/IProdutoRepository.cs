using DEMO_DDD.DOMAIN.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DEMO_DDD.DOMAIN.Interfaces.Repositories
{
    public interface IProdutoRepository : IRepositoryBase<Produto>
    {
         Task<IEnumerable<Produto>> BuscarPorNome(string nome);
    }
}
