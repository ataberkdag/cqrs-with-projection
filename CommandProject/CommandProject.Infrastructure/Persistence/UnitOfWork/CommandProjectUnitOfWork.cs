using CommandProject.Domain.Interfaces;
using CommandProject.Domain.Interfaces.Repositories;
using CommandProject.Infrastructure.Persistence.Context;
using CommandProject.Infrastructure.Persistence.Repositories;
using Shared.Domain.Interfaces;

namespace CommandProject.Infrastructure.Persistence.UnitOfWork
{
    public class CommandProjectUnitOfWork : ICommandProjectUnitOfWork
    {
        public ITodoItemRepository TodoItems { get; }

        public IOutboxMessageRepository OutboxMessages { get; }

        private readonly CommandProjectDbContext _ctx;
        public CommandProjectUnitOfWork(CommandProjectDbContext ctx)
        {
            _ctx = ctx;

            TodoItems = new TodoItemRepository(_ctx);
            OutboxMessages = new OutboxMessageRepository(_ctx);
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
