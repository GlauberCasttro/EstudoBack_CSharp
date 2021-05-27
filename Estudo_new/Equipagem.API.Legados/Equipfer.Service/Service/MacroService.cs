using Equipfer.Domain.Entity.MacroAggregate;
using Equipfer.Domain.Interface;
using Equipfer.Domain.Repository;
using Equipfer.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Equipfer.Service.Service
{
    public class MacroService : IMacroService
    {

        private readonly IMacroRepository _repository;


        public MacroService(IMacroRepository macroRepository)
        {
            this._repository = macroRepository;
        }
       

        public async Task<List<Macro>> GetMacroPendentesAsync(string matricula)
        {
            return await _repository.GetMacrosPendentesAsync(matricula);
        }


        public Task<List<Macro>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Macro> GetAsync(Macro entidade)
        {
            throw new NotImplementedException();
        }
    }
}
