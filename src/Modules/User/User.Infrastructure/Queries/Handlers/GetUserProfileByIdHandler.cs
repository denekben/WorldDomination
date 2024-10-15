using User.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Infrastructure.Contexts;
using User.Infrastructure.ReadModels;
using User.Application.Users.Queries;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace User.Infrastructure.Queries.Handlers
{
    internal class GetUserProfileByIdHandler : IRequestHandler<GetUserProfileById, UserProfileDto>
    {
        private readonly DbSet<UserReadModel> _users;

        public GetUserProfileByIdHandler(UserReadDbContext context)
        {
            _users = context.Users;
        }

        public async Task<UserProfileDto> Handle(GetUserProfileById query, CancellationToken cancellationToken)
        {
            var users = await _users
                .Where(u => u.Id == query.userId)
                .Select(u => u.AsDto())
                .SingleOrDefaultAsync() ??
                throw new NotFoundException($"User {query.userId} not found");

            return users;
        }
    }
}
