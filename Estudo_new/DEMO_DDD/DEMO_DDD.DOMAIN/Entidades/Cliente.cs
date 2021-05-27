using System;
using System.Collections.Generic;
using System.Text;

namespace DEMO_DDD.DOMAIN.Entidades
{
   public class Cliente : Entity
    {
       // public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<Produto> Produtos { get; set; }




        /// <summary>
        /// Regra de negocio para cliente especial. Cliente cadastrado a mais de 5 anos.
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public bool ClienteEspecial(Cliente cliente)
        {
            return cliente.Ativo && DateTime.Now.Year - cliente.DataCadastro.Year >= 5;
        }
    }
}
