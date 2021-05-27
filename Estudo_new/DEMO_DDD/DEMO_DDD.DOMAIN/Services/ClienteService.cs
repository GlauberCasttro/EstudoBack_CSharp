using DEMO_DDD.DOMAIN.Entidades;
using DEMO_DDD.DOMAIN.Interfaces.Repositories;
using DEMO_DDD.DOMAIN.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO_DDD.DOMAIN.Services
{
    public class ClienteService : ServiceBase<Cliente>, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository) : base(clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }


        //Busca clientes especiais, regra de negocio na classe cliente
        public  IEnumerable<Cliente> ObterClientesEspeciais(IEnumerable<Cliente> clientes)
        {
            return clientes.Where(c => c.ClienteEspecial(c));
        }

        //Busca os produtos e seus respectivos produtos
        public async Task<IEnumerable<Cliente>> ObterClientesProdutos()
        {
            return await _clienteRepository.ObterClientesProdutos();
        }

        public async Task<IEnumerable<Cliente>> ObterPorNome(string nome, DateTime? date)
        {
            return await _clienteRepository.ObterPorNome(nome, date);
        }
    }
}
