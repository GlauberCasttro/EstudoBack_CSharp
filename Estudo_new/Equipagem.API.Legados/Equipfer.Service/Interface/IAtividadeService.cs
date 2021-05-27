using Equipfer.Domain.Entity;
using Equipfer.Domain.Entity.EquipagemAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Equipfer.Service.Interface
{
    public interface IAtividadeService : IService<Atividade>
    {
        Task<List<AtividadeJornada>> AtividadeJornadaAtualAsync(Equipagem entidade);
        Task<List<AtividadeJornada>> AtividadeJornadaAnteriorAsync(Equipagem entidade);
        Task<JornadaMensalAcumulada> AtividadesQuizenalMensalAsync(Equipagem equipagem);
    }
}
