using DEMO_DDD.DOMAIN.Entidades;
using DEMO_DDD.DOMAIN.Interfaces.Repositories;
using DEMO_DDD.DOMAIN.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DEMO_DDD.DOMAIN.Services
{
    public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : Entity
    {
        private readonly IRepositoryBase<TEntity> _repository;
        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }
        public async Task Adcionar(TEntity entity)
        {
            await _repository.Adcionar(entity);
        }

        public async Task Atualizar(TEntity entity)
        {
            await _repository.Atualizar(entity);
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.Buscar(predicate);
        }


        public async Task<TEntity> ObetrPorId(int id)
        {
            return await _repository.ObetrPorId(id);
        }

        public async Task<IEnumerable<TEntity>> ObterTodos()
        {
            return await _repository.ObterTodos();
        }

        public async Task Remover(int id)
        {
            await _repository.Remover(id);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
