using Shared.Domain.Base;

namespace Shared.Domain.Interfaces
{
    public interface IAggregateRoot
    {
        public IReadOnlyCollection<DomainEvent> DomainEvents { get; }
        void AddDomainEvent(DomainEvent domainEvent);
        void RemoveDomainEvent(DomainEvent domainEvent);
        void ClearDomainEvents();
    }
}
