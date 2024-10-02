using AppUser.Shared.DTOs;
using MediatR;
using System;

namespace AppUser.Application.Queries
{
    public record GetUserProfile(Guid id) : IRequest<UserProfileDto>;
}
