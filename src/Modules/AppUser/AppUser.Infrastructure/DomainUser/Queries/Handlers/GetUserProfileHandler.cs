using AppUser.Application.Queries;
using AppUser.Infrastructure.DomainUser.Contexts;
using AppUser.Infrastructure.DomainUser.ReadModels;
using AppUser.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppUser.Infrastructure.DomainUser.Queries.Handlers
{
    internal class GetUserProfileHandler : IRequestHandler<GetUserProfile, UserProfileDto>
    {
        private readonly DbSet<UserReadModel> _users;

        public GetUserProfileHandler(UserReadDbContext context)
        {
            _users = context.Users;
        }

        public async Task<UserProfileDto> Handle(GetUserProfile query, CancellationToken cancellationToken)
        {
            return await _users
                .Include(u => u.ActivityStatusReadModel)
                .Include(u => u.UserAchievments)
                .Where(u => u.Id == query.id)
                .Select(u => u.AsDto())
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }
    }
}
