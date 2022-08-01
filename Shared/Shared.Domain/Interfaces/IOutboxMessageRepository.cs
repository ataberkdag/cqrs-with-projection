using Shared.Domain.Entities;

namespace Shared.Domain.Interfaces
{
    public interface IOutboxMessageRepository : IRepository<OutboxMessage>
    {
    }
}
