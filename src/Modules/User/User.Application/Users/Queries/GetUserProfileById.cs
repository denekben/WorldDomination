using User.Shared.DTOs;
using MediatR;
using System;

namespace User.Application.Users.Queries
{
    public record GetUserProfileById(Guid id) : IRequest<UserDto>;
}
