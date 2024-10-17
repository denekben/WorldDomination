using User.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Infrastructure.Contexts;
using User.Infrastructure.ReadModels;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using User.Application.User.Queries;
using WorldDomination.Shared.Services;

namespace User.Infrastructure.Queries.Handlers
{
    internal class GeCurrentUserProfileByIdHandler : IRequestHandler<GetCurrentUserProfile, Profile>
    {
        private readonly DbSet<UserReadModel> _users;
        private readonly IHttpContextService _contextService;

        public GeCurrentUserProfileByIdHandler(UserReadDbContext context, IHttpContextService contextService)
        {
            _users = context.Users;
            _contextService = contextService;
        }

        public async Task<Profile>? Handle(GetCurrentUserProfile query, CancellationToken cancellationToken)
        {
            var userId = _contextService.GetCurrentUserId();
            var users = _users.Where(u => u.Id == userId);

            if (users.Count() != 1)
            {
                throw new NotFoundException("Cannot find user");
            }

            var profile = await users
                .Include(u => u.UserAchievmentsReadModel).ThenInclude(ua => ua.AchievmentReadModel)
                .Select(u => new Profile(
                    u.AsUserDto(),
                    u.UserAchievmentsReadModel.Select(ua => ua.AsUserAchievmentDto()).ToList()
                )).SingleOrDefaultAsync();

            return profile;
        }
    }
}