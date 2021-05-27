using Equipfer.Domain.Entity;
using Equipfer.Domain.Entity.EquipagemAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Equipfer.Domain.Interface
{
    public interface IEquipagemRepository : IRepository<Equipagem>
    {
        Task<List<string>> GetAllDisponiveisAsync(string codigo);
    }
}
