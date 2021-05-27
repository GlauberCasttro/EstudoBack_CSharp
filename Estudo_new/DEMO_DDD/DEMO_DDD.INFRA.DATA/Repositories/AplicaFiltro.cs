using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using DEMO_DDD.DOMAIN.Entidades;
using DEMO_DDD.INFRA.DATA.Contexto;

namespace DEMO_DDD.INFRA.DATA.Repositories
{
    public abstract class AplicaFiltro
    {
        public readonly DemoDDDContext _context;

        public AplicaFiltro(DemoDDDContext context)
        {
            _context = context;
        }
    }

    public class FiltroData : AplicaFiltro
    {
        public FiltroData(DemoDDDContext context) : base(context)
        {
            
        }
        public void AplicarFiltro(Expression<Func<Cliente, bool>> predicate)
        {

        }
    }

}
