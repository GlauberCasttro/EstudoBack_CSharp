using DEMO_DDD.DOMAIN.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DEMO_DDD.APPLICATION.Interfaces
{
    public interface IProdutoAppService
    {
        Task<IEnumerable<Produto>> BuscarPorNome(string nome);
    }
}
