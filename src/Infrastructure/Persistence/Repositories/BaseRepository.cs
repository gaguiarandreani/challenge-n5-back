using Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecurityApi.Infrastructure.Persistence.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        protected readonly AppDbContext _dbcontext;

        public BaseRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return _dbcontext.Set<TEntity>();
        }

        public async Task<TEntity> ModifyAsync(TEntity entity)
        {
            var dbset = _dbcontext.Set<TEntity>();

            dbset.Update(entity);

            await _dbcontext.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var dbset = _dbcontext.Set<TEntity>();

            dbset.Add(entity);

            await _dbcontext.SaveChangesAsync();

            return entity;
        }
    }
}