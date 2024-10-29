using MediatR;
using System.Collections.Generic;

namespace WorldDomination.Shared.Domain
{
    public abstract class DomainEntity
    {
        private readonly List<INotification> _domainEvents = [];
        public IReadOnlyList<INotification> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(INotification domainEvent)
            => _domainEvents.Add(domainEvent);

        public void RemoveDomainEvent(INotification domainEvent)
            => _domainEvents.Remove(domainEvent);
    }
}
