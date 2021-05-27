using DEMO_DDD.DOMAIN.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DEMO_DDD.DOMAIN.Interfaces.Repositories
{
    /// <summary>
    /// Repositorio genérico
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : Entity
    {
        Task Adcionar(TEntity entity);
        Task<TEntity> ObetrPorId(int id);
        Task<List<TEntity>> ObterTodos();
        Task Atualizar(TEntity entity);
        Task Remover(int id);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();

    }
}
