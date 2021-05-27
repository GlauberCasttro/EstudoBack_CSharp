using Equipfer.Domain.Entity.MacroAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Equipfer.Service.Interface
{
   public interface IMacroService : IService<Macro>
    {
        Task<List<Macro>> GetMacroPendentesAsync(string matricula);
    }
}
