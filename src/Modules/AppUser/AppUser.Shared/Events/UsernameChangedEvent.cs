using Shared.Events;

namespace AppUser.Shared.Events
{
    public record UsernameChangedEvent(string UserId, string Username) : IEvent
    {
    }
}
