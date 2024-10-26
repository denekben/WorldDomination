using User.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Infrastructure.Contexts;
using User.Infrastructure.ReadModels;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using User.Application.User.Queries;

namespace User.Infrastructure.Queries.Handlers
{
    internal class GetProfileByIdHandler : IRequestHandler<GetProfileById, Profile>
    {
        private readonly DbSet<UserReadModel> _users;

        public GetProfileByIdHandler(UserReadDbContext context)
        {
            _users = context.Users;
        }

        public async Task<Profile>? Handle(GetProfileById query, CancellationToken cancellationToken)
        {
            var users = _users.Where(u=>u.Id == query.id);
            
            if (users.Count() != 1)
            {
                throw new NotFoundException("Cannot find user");
            }
            
            var profile = await users
                .Include(u=>u.UserAchievments).ThenInclude(ua=>ua.Achievment)
                .Select(u => new Profile (
                    u.AsUserDto(),
                    u.UserAchievments.Select(ua => ua.AsUserAchievmentDto()).ToList()
                )).SingleOrDefaultAsync();

            return profile;
        }
    }
}