using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Application.User.Queries;
using User.Infrastructure.Contexts;
using User.Infrastructure.ReadModels;
using User.Shared.DTOs;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using WorldDomination.Shared.Services;

namespace User.Infrastructure.Queries.Handlers
{
    internal class GetCurrentUserAchievmentsHandler : IRequestHandler<GetCurrentUserAchievments, List<UserAchievmentDto>?>
    {
        private readonly DbSet<UserReadModel> _users;
        private readonly IHttpContextService _contextService;

        public GetCurrentUserAchievmentsHandler(UserReadDbContext context, IHttpContextService contextService)
        {
            _users = context.Users;
            _contextService = contextService;
        }

        public async Task<List<UserAchievmentDto>?> Handle(GetCurrentUserAchievments query, CancellationToken cancellationToken)
        {
            var userId = _contextService.GetCurrentUserId();

            var users = _users.Where(u => u.Id == userId);

            if (users.Count() != 1)
            {
                throw new NotFoundException("Cannot find user");
            }

            var userAchievments = await users
                .Include(u => u.UserAchievments).ThenInclude(ua => ua.Achievment)
                .Select(u => u.UserAchievments.Select(ua => ua.AsUserAchievmentDto()).ToList()).SingleOrDefaultAsync();

            return userAchievments;
        }
    }
}
