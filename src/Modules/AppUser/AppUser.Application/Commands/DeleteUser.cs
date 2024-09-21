using Shared.Commands;
using System;

namespace AppUser.Application.Commands
{
    public record DeleteUser(Guid Id) : ICommand
    {
    }
}
