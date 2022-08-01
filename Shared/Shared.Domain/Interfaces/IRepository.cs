namespace Shared.Domain.Interfaces
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task AddAsync(T item);
        Task<T> GetItemById(Guid id);
        IEnumerable<T> GetByQuery(Func<T, bool> expression);
    }
}
