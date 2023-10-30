using Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecurityApi.Infrastructure.Persistence.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        protected readonly DbContextOptionsBuilder<AppDbContext> _builder;

        public BaseRepository(AppDbContextOptionsBuilder builder)
        {
            _builder = builder.CreateNew();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var dbcontext = new AppDbContext(_builder.Options);

            return dbcontext.Set<TEntity>();
        }

        public virtual async Task<TEntity> FindAsync(object id)
        {
            using (var dbcontext = new AppDbContext(_builder.Options))
            {
                return await dbcontext.Set<TEntity>().FindAsync(id);
            }
        }

        public async Task<TEntity> ModifyAsync(TEntity entity)
        {
            using (var dbcontext = new AppDbContext(_builder.Options))
            {
                var dbset = dbcontext.Set<TEntity>();

                dbset.Update(entity);

                await dbcontext.SaveChangesAsync();

                return entity;
            }
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            using (var dbcontext = new AppDbContext(_builder.Options))
            {
                var dbset = dbcontext.Set<TEntity>();

                dbset.Add(entity);

                await dbcontext.SaveChangesAsync();

                return entity;
            }
        }
    }
}