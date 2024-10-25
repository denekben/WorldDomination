using Shared.Events;

namespace User.Shared.IntegrationEvents
{
    public sealed record NewDomainUserCreated(Guid UserId, string Username, string ProfileImagePath) : IEvent;
}
