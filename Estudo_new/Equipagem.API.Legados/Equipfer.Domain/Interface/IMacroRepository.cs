using Equipfer.Domain.Entity.MacroAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Equipfer.Domain.Interface
{
    public interface IMacroRepository : IRepository<Macro>
    {
        Task<List<Macro>> GetMacrosPendentesAsync(string matricula);
    }
}
