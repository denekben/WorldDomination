using Shared.Events;

namespace AppUser.Shared.Events
{
    public record NewUserRegistered(string Id, string Username, string Email) : IEvent;
}
