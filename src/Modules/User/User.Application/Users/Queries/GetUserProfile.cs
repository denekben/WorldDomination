using User.Shared.DTOs;
using MediatR;
using System;

namespace User.Application.Users.Queries
{
    public record GetUserProfile(Guid id) : IRequest<UserProfileDto?>;
}
