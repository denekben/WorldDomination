using User.Shared.DTOs;
using MediatR;
using System;

namespace User.Application.User.Queries
{
    public record GetProfileById(Guid id) : IRequest<Profile>;
}
