using AoTTG2.IDS.Data.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoTTG2.IDS.Data.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : Entity
    {
        Task AddAsync(TEntity entity);
        Task<TEntity> GetAsync(Guid id);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(Guid id);
    }
}
