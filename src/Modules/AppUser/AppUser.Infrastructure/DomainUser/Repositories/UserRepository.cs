using AppUser.Domain.Repositories;
using AppUser.Infrastructure.DomainUser.Contexts;
using Microsoft.EntityFrameworkCore;
using UserAccess.Domain.Entities;

namespace AppUser.Infrastructure.DomainUser.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<User> _users;
        private readonly UserWriteDbContext _context;

        public UserRepository(UserWriteDbContext context)
        {
            _users = context.Users;
            _context = context;
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await _users.Include(u => u.ActivityStatus).FirstOrDefaultAsync(u=>u.Id.Value == id);
        }

        public async Task AddAsync(User user)
        {
            await _users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
