using CommandProject.Domain.Interfaces.Repositories;
using Shared.Domain.Interfaces;

namespace CommandProject.Domain.Interfaces
{
    public interface ICommandProjectUnitOfWork : IUnitOfWork
    {
        ITodoItemRepository TodoItems { get; }
    }
}
