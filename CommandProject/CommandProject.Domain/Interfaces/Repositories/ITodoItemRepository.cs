using CommandProject.Domain.Entities;
using Shared.Domain.Interfaces;

namespace CommandProject.Domain.Interfaces.Repositories
{
    public interface ITodoItemRepository : IRepository<TodoItem>
    {
    }
}
