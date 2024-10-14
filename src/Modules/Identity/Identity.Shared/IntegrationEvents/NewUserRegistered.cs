using Shared.Events;

namespace Identity.Shared.IntegrationEvents
{
    public sealed record NewUserRegistered(string UserId, string Username, string Email) : IEvent;
}
