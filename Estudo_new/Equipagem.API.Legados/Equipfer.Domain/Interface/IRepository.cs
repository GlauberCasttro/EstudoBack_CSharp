using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Equipfer.Domain.Interface
{
    public interface IRepository<T>
    {
        Task<T> GetAsync(T entidade);
        Task<List<T>> GetAllAsync();
    }
}
