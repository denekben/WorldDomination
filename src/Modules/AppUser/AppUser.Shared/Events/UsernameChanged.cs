using Shared.Events;

namespace AppUser.Shared.Events
{
    public record UsernameChanged(Guid Id, string Username) : IEvent
    {
    }
}
