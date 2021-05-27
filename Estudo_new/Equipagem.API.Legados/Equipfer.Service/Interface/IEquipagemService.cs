using Equipfer.Domain.Entity;
using Equipfer.Domain.Entity.EquipagemAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Equipfer.Service.Interface
{
    public interface IEquipagemService : IService<Equipagem>
    {
        Task<List<string>> GetAllDisponiveisAsync(string codigo);
    }
}
