using AoTTG2.IDS.Data.Dao;
using AoTTG2.IDS.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoTTG2.IDS.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _baseQuery;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _baseQuery = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            _baseQuery.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _baseQuery.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var toDelete = await GetAsync(id);
            await DeleteAsync(toDelete);
        }

        public async Task<TEntity> GetAsync(Guid Id)
        {
            var entity = await _baseQuery.SingleOrDefaultAsync(e => e.Id == Id);

            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _baseQuery.Attach(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
