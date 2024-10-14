using Shared.Events;

namespace Identity.Shared.IntegrationEvents
{
    public sealed record UserDeleted(string UserId) : IEvent;
}
