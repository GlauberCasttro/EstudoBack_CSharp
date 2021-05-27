using Equipfer.Domain.Entity;
using Equipfer.Domain.Entity.EquipagemAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Equipfer.Domain.Interface
{
    public interface ITarefaRepository : IRepository<Tarefa>
    {
        Task<List<TarefaEscalaProgramada>> EscalaMensalAsync(Equipagem equipagem);
    }
}
