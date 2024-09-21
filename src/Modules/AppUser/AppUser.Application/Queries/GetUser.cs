using AppUser.Shared.DTOs;
using Shared.Queries;
using System;

namespace AppUser.Application.Queries
{
    public record GetUser(Guid id) : IQuery<GetUserDto>
    {
    }
}
