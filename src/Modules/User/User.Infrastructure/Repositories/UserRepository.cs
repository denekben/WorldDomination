using Microsoft.EntityFrameworkCore;
using User.Domain.Entities;
using WorldDomination.Shared.Domain;
using User.Infrastructure.Contexts;

namespace User.Infrastructure.Repositories
{
    public class UserRepository : IRepository<DomainUser>
    {
        private readonly DbSet<DomainUser> _users;
        private readonly UserWriteDbContext _context;

        public UserRepository(UserWriteDbContext context)
        {
            _users = context.Users;
            _context = context;
        }

        public async Task<DomainUser?> GetAsync(IdValueObject id)
        {
            return await _users.Include(u => u.UserStatus).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddAsync(DomainUser user)
        {
            await _users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DomainUser user)
        {
            _users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(DomainUser user)
        {
            _users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
