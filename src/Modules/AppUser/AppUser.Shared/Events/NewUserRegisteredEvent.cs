using Shared.Events;

namespace AppUser.Shared.Events
{
    public record NewUserRegisteredEvent(string UserId, string Username, string Email) : IEvent;
}
