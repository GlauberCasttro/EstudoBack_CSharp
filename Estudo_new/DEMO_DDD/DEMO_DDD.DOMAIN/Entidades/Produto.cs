using System;
using System.Collections.Generic;
using System.Text;

namespace DEMO_DDD.DOMAIN.Entidades
{
   public class Produto : Entity
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public bool  Disponivel { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
