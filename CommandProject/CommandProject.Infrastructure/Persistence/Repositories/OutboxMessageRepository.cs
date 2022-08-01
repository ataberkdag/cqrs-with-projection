using CommandProject.Infrastructure.Persistence.Context;
using Shared.Domain.Entities;
using Shared.Domain.Interfaces;

namespace CommandProject.Infrastructure.Persistence.Repositories
{
    public class OutboxMessageRepository : Repository<OutboxMessage>, IOutboxMessageRepository
    {
        public OutboxMessageRepository(CommandProjectDbContext ctx) : base(ctx)
        {

        }
    }
}
