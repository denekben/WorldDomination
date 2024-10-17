using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Application.User.Queries;
using User.Infrastructure.Contexts;
using User.Infrastructure.ReadModels;
using User.Shared.DTOs;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace User.Infrastructure.Queries.Handlers
{
    internal class GetUserAchievmentsByIdHandler : IRequestHandler<GetUserAchievmentsById, List<UserAchievmentDto>?>
    {
        private readonly DbSet<UserReadModel> _users;

        public GetUserAchievmentsByIdHandler(UserReadDbContext context)
        {
            _users = context.Users;
        }

        public async Task<List<UserAchievmentDto>?> Handle(GetUserAchievmentsById query, CancellationToken cancellationToken)
        {
            var users = _users.Where(u => u.Id == query.id);

            if (users.Count() != 1)
            {
                throw new NotFoundException("Cannot find user");
            }

            var userAchievments = await users
                .Include(u => u.UserAchievmentsReadModel).ThenInclude(ua => ua.AchievmentReadModel)
                .Select(u => u.UserAchievmentsReadModel.Select(ua => ua.AsUserAchievmentDto()).ToList()).SingleOrDefaultAsync();

            return userAchievments;
        }
    }
}
