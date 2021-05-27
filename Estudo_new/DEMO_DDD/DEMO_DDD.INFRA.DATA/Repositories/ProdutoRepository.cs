using DEMO_DDD.DOMAIN.Entidades;
using DEMO_DDD.DOMAIN.Interfaces.Repositories;
using DEMO_DDD.INFRA.DATA.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEMO_DDD.INFRA.DATA.Repositories
{
    /// <summary>
    /// Classe de repositorio
    /// </summary>
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public ProdutoRepository(DemoDDDContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Produto>> BuscarPorNome(string nome)
        {
            return await Buscar(p => p.Nome.Contains(nome));
        }
    }
}
