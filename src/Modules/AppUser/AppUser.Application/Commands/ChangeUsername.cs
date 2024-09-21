using Shared.Commands;
using System;

namespace AppUser.Application.Commands
{
    public record ChangeUsername(Guid Id, string Username) : ICommand
    {
    }
}
