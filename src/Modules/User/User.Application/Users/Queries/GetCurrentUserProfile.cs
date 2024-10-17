using User.Shared.DTOs;
using MediatR;
using System;

namespace User.Application.User.Queries
{
    public record GetCurrentUserProfile : IRequest<Profile>;
}
