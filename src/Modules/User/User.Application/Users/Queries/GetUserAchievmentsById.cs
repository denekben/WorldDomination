using MediatR;
using User.Shared.DTOs;

namespace User.Application.User.Queries
{
    public sealed record GetUserAchievmentsById(Guid id) : IRequest<List<UserAchievmentDto>?>;
}