using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Application.User.Queries;
using User.Infrastructure.Contexts;
using User.Infrastructure.ReadModels;
using User.Shared.DTOs;

namespace User.Infrastructure.Queries.Handlers
{
    internal class SearchUsersHandler : IRequestHandler<SearchUsers, List<SearchUserDto>?>
    {
        private readonly DbSet<UserReadModel> _users;

        public SearchUsersHandler(UserReadDbContext context)
        {
            _users = context.Users;
        }

        public async Task<List<SearchUserDto>?> Handle(SearchUsers query, CancellationToken cancellationToken)
        {
            var users = _users.AsQueryable();

            if(!string.IsNullOrEmpty(query.SearchPhrase))
            {
                users = users.Where(u
                => EF.Functions.ILike(u.Username, $"%{query.SearchPhrase}%"));
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await users.Skip(skipNumber).Take(query.PageSize).Select(u=>u.AsSearchUserDto()).ToListAsync();

        }
    }
}
