using Shared.Events;

namespace AppUser.Shared.Events
{
    public record UserDeleted(Guid Id) : IEvent
    {
    }
}
