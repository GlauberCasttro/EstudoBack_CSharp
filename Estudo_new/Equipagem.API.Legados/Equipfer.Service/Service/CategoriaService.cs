using Equipfer.Domain.Entity;
using Equipfer.Domain.Interface;
using Equipfer.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Equipfer.Service.Service
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repository;
        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Categoria> GetAsync(Categoria entidade)
        {
            return await _repository.GetAsync(entidade);
        }

        public async Task<List<Categoria>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
