using CommandProject.Domain.Entities;
using CommandProject.Domain.Interfaces.Repositories;
using CommandProject.Infrastructure.Persistence.Context;

namespace CommandProject.Infrastructure.Persistence.Repositories
{
    public class TodoItemRepository : Repository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(CommandProjectDbContext ctx) : base(ctx)
        {

        }
    }
}
