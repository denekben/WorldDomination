using Shared.Commands;

namespace AppUser.Application.Commands
{
    public record RegisterNewUser(string Username) : ICommand
    {
    }
}
