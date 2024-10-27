using User.Shared.DTOs;
using MediatR;

namespace User.Application.User.Queries
{
    public record GetCurrentUserProfile : IRequest<Profile>;
}