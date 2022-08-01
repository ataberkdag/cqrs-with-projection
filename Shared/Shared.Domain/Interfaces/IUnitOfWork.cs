namespace Shared.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IOutboxMessageRepository OutboxMessages { get; }

        Task SaveChangesAsync();
    }
}
