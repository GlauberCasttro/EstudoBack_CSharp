using Equipfer.Domain.Entity;
using Equipfer.Domain.Entity.EquipagemAggregate;
using Equipfer.Domain.Interface;
using Equipfer.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Equipfer.Service.Service
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _repository;
        public TarefaService(ITarefaRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<TarefaEscalaProgramada>> EscalaMensalAsync(Equipagem equipagem)
        {
            return await _repository.EscalaMensalAsync(equipagem);
        }
        public async Task<Tarefa> GetAsync(Tarefa entidade)
        {
            return await _repository.GetAsync(entidade);
        }
        public async Task<List<Tarefa>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
