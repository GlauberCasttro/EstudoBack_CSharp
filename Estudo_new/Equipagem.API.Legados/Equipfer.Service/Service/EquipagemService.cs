using Equipfer.Domain.Entity;
using Equipfer.Domain.Entity.EquipagemAggregate;
using Equipfer.Domain.Interface;
using Equipfer.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Equipfer.Service.Service
{
    public class EquipagemService : IEquipagemService
    {
        private IEquipagemRepository _equipagemRepository;

        public EquipagemService(IEquipagemRepository equipagemRepository)
        {
            _equipagemRepository = equipagemRepository;
        }

        public async Task<List<string>> GetAllDisponiveisAsync(string codigo)
        {
            return await _equipagemRepository.GetAllDisponiveisAsync(codigo);
        }

        public async Task<Equipagem> GetAsync(Equipagem matricula)
        {
            return await _equipagemRepository.GetAsync(matricula);
        }

        public Task<List<Equipagem>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
