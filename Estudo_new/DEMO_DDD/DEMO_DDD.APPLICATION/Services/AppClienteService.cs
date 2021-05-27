using DEMO_DDD.APPLICATION.Interfaces;
using DEMO_DDD.DOMAIN.Entidades;
using DEMO_DDD.DOMAIN.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DEMO_DDD.APPLICATION.ViewModels;

namespace DEMO_DDD.APPLICATION.Services
{
    public class AppClienteService : AppServiceBase<Cliente>, IClienteAppService
    {
        //Camada de dominio
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;
        public AppClienteService(IClienteService clienteService, IMapper mapper) : base(clienteService)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Cliente>> ObterClientesEspeciaisAsync()
        {
            return _clienteService.ObterClientesEspeciais(await _clienteService.ObterTodos());
        }

        public async Task<IEnumerable<Cliente>> ObterClientesProdutos()
        {
            return await _clienteService.ObterClientesProdutos();
        }

        public async Task<IEnumerable<Cliente>> ObterPorNome(string nome, DateTime? date)
        {
            return await _clienteService.ObterPorNome(nome, date);
        }
    }
}
