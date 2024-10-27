using User.Shared.DTOs;
using MediatR;

namespace User.Application.User.Queries
{
    public record GetProfileById(Guid id) : IRequest<Profile>;
}