using System;
using System.Collections.Generic;
using System.Text;

namespace DEMO_DDD.DOMAIN.Entidades
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
