using Equipfer.Domain.Entity;
using Equipfer.Domain.Entity.EquipagemAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Equipfer.Domain.Interface
{
    public interface IAtividadeRepository:IRepository<Atividade>
    {
        Task<List<AtividadeJornada>> AtividadeJornadaAtualAsync(Equipagem entidade);
        Task<List<AtividadeJornada>> AtividadeJornadaAnteriorAsync(Equipagem entidade);
        Task<List<AtividadeJornada>> AtividadesQuizenalMensalAsync(Equipagem equipagem);
    }
}
