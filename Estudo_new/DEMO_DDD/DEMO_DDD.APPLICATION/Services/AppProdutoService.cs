using DEMO_DDD.APPLICATION.Interfaces;
using DEMO_DDD.DOMAIN.Entidades;
using DEMO_DDD.DOMAIN.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DEMO_DDD.APPLICATION.Services
{
    public class AppProdutoService : AppServiceBase<Produto>, IProdutoAppService
    {
        private readonly IProdutoService _produtoService;
        public AppProdutoService(IProdutoService produtoService) : base(produtoService)
        {
            _produtoService = produtoService;
        }
        public async Task<IEnumerable<Produto>> BuscarPorNome(string nome)
        {
            return await _produtoService.BuscarPorNome(nome);
        }
    }
}
