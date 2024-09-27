using Shared.Events;

namespace AppUser.Shared.Events
{
    public record UserDeleted(string UserId) : IEvent
    {
    }
}
