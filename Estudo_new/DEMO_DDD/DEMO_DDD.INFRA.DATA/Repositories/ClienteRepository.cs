using System;
using DEMO_DDD.DOMAIN.Entidades;
using DEMO_DDD.DOMAIN.Interfaces.Repositories;
using DEMO_DDD.INFRA.DATA.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DEMO_DDD.INFRA.DATA.Repositories
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {

        public ClienteRepository(DemoDDDContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Cliente>> ObterClientesProdutos()
        {
            return await _context.Clientes.AsNoTracking().Include(c => c.Produtos).ToListAsync();
            
           
        }

        public async Task<IEnumerable<Cliente>> ObterPorNome(string nome, DateTime? date)
        {
           
                if (date.Equals(null) || date.Equals(""))
                    throw new Exception("A data do filtro nao pode ser nula");
            
          





            if (!string.IsNullOrEmpty(nome) && date.Equals(null))
            {

                return await _context.Clientes.Where(c => c.Nome.Contains(nome) && c.DataCadastro == date).ToListAsync();
            }
            else
            {
                return await _context.Clientes.Where(c => c.Nome.Contains(nome)).ToListAsync();
            }

        }

        public async Task<Produto> ObterProdutosPorCliente(int id)
        {
            return await _context.Produtos.AsNoTracking().Include(c => c.Cliente).FirstOrDefaultAsync(c => c.Id == id);
        }
    }

    public class DomainException : Exception
    {
        public DomainException()
        { }

        public DomainException(string message) : base(message)
        { }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        { }
    }

}
