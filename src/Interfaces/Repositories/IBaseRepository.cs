namespace Interfaces.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> ModifyAsync(TEntity entity);

        Task<TEntity> CreateAsync(TEntity entity);
    }
}