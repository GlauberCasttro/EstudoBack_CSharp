using DEMO_DDD.DOMAIN.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DEMO_DDD.DOMAIN.Interfaces.Services
{
    public interface IServiceBase<TEntity> : IDisposable where TEntity : Entity
    {
        Task Adcionar(TEntity entity);
        Task<TEntity> ObetrPorId(int id);
        Task<IEnumerable<TEntity>> ObterTodos();
        Task Atualizar(TEntity entity);
        Task Remover(int id);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
    }
}
