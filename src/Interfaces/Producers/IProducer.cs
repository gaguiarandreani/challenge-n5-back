namespace Interfaces.Producers
{
    public interface IProducer<TEntity>
    {
        Task<bool> ProduceAsync(TEntity entity);
    }
}