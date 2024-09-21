using AppUser.Application.Queries;
using AppUser.Shared.DTOs;
using Shared.Queries;

namespace AppUser.Infrastructure.EF.Queries.Handlers
{
    public class GetUserHandler : IQueryHandler<GetUser, GetUserDto>
    {
        public Task<GetUserDto> HandleAsync(GetUser query, CancellationToken cancellationToken = default)
        {
            // need to add ef
        }
    }
}
