using User.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Infrastructure.Contexts;
using User.Infrastructure.ReadModels;
using User.Application.Users.Queries;

namespace User.Infrastructure.Queries.Handlers
{
    internal class GetUserProfileHandler : IRequestHandler<GetUserProfile, UserProfileDto?>
    {
        private readonly DbSet<UserReadModel> _users;

        public GetUserProfileHandler(UserReadDbContext context)
        {
            _users = context.Users;
        }

        public async Task<UserProfileDto?> Handle(GetUserProfile query, CancellationToken cancellationToken)
        {
            return await _users
                .Where(u => u.Id == query.id)
                .Select(u => u.AsDto())
                .SingleOrDefaultAsync();
        }
    }
}
