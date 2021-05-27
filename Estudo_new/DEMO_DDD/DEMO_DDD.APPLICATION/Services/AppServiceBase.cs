using AutoMapper;
using DEMO_DDD.APPLICATION.Interfaces;
using DEMO_DDD.DOMAIN.Entidades;
using DEMO_DDD.DOMAIN.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DEMO_DDD.APPLICATION.Services
{
    public abstract class AppServiceBase<TEntity> : IDisposable, IAppServiceBase<TEntity> where TEntity : Entity
    {
        private readonly IServiceBase<TEntity> _serviceBase;

        public AppServiceBase(IServiceBase<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public async Task Adcionar(TEntity entity)
        {
            await _serviceBase.Adcionar(entity);
        }

        public async Task Atualizar(TEntity entity)
        {
            await _serviceBase.Atualizar(entity);
        }
        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await _serviceBase.Buscar(predicate);
        }

        public Task<TEntity> ObetrPorId(int id)
        {
            return _serviceBase.ObetrPorId(id);
        }

        public virtual async Task<IEnumerable<TEntity>> ObterTodos()
        {
            return await _serviceBase.ObterTodos();
        }

        public async Task Remover(int id)
        {
            await _serviceBase.Remover(id);
        }
        public void Dispose()
        {
            _serviceBase?.Dispose();
        }
    }
}
