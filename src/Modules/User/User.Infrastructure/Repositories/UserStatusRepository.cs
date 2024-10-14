using User.Domain.Entities;
using User.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Domain;
using User.Infrastructure.Contexts;

namespace User.Infrastructure.Repositories
{
    public class UserStatusRepository : IUserStatusRepository
    {
        private readonly DbSet<UserStatus> _userStatuses;
        private readonly UserWriteDbContext _context;

        public UserStatusRepository(UserWriteDbContext context)
        {
            _userStatuses = context.UserStatuses;
            _context = context;
        }

        public async Task AddAsync(UserStatus userStatus)
        {
            await _userStatuses.AddAsync(userStatus);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserStatus userStatus)
        {
            _userStatuses.Remove(userStatus);
            await _context.SaveChangesAsync();
        }

        public async Task<UserStatus?> GetAsync(IdValueObject id)
        {
            return await _userStatuses.FirstOrDefaultAsync(status => status.UserId == id);
        }

        public async Task UpdateAsync(UserStatus userStatus)
        {
            _userStatuses.Update(userStatus);
            await _context.SaveChangesAsync();
        }
    }
}
