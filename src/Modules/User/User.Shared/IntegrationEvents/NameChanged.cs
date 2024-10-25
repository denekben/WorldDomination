using Shared.Events;

namespace User.Shared.IntegrationEvents
{
    public sealed record NameChanged(Guid UserId, string Name) : IEvent;
}
