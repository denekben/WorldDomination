using AppUser.Shared.DTOs;
using MediatR;
using System;

namespace AppUser.Application.Queries
{
    public record GetUser(Guid id) : IRequest<UserDto>
    {
    }
}
