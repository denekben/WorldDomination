using Shared.Events;

namespace AppUser.Shared.Events
{
    public record NewUserRegistered(Guid Id, string Username) : IEvent;
}
