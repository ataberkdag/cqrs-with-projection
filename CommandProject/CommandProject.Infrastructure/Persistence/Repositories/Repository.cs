using Microsoft.EntityFrameworkCore;
using Shared.Domain.Interfaces;

namespace CommandProject.Infrastructure.Persistence.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        private readonly DbContext _ctx;
        public Repository(DbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(T item)
        {
            await _ctx.Set<T>().AddAsync(item);
        }

        public IEnumerable<T> GetByQuery(Func<T, bool> expression)
        {
            return _ctx.Set<T>().Where(expression).ToList();
        }

        public async Task<T> GetItemById(Guid id)
        {
            return await _ctx.Set<T>().FindAsync(id);
        }
    }
}
