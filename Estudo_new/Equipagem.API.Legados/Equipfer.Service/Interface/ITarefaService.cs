using Equipfer.Domain.Entity;
using Equipfer.Domain.Entity.EquipagemAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Equipfer.Service.Interface
{
    public interface ITarefaService: IService<Tarefa>
    {
        Task<List<TarefaEscalaProgramada>> EscalaMensalAsync(Equipagem equipagem);
    }
}
