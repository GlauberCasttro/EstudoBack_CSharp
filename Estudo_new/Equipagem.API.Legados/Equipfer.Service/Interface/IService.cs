using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Equipfer.Service.Interface
{
    public interface IService<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(T entidade);
    }
}
