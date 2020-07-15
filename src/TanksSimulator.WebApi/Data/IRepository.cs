using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TanksSimulator.WebApi.Data
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAsync();
        Task<T> GetByIdAsync(string id);
        Task<T> CreateAsync(T model);
        Task<T> UpdateAsync(T model);
    }
}
