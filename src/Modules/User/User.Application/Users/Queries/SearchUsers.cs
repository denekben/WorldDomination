using MediatR;
using User.Shared.DTOs;

namespace User.Application.User.Queries
{
    public sealed record SearchUsers(string? SearchPhrase, int PageNumber = 1, int PageSize = 10) : IRequest<List<SearchUserDto>>;
}