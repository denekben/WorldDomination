using Game.Domain.DomainModels.Users.Entities;
using Game.Domain.Interfaces.Repositories;
using Game.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Domain;

namespace Game.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<GameUser> _users;
        private readonly GameWriteDbContext _context;

        public UserRepository(GameWriteDbContext context)
        {
            _users = context.Users;
            _context = context;
        }

        public async Task AddAsync(GameUser user)
        {
            await _users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(GameUser user)
        {
            _users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<GameUser?> GetAsync(IdValueObject id)
        {
            var user = await _users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task UpdateAsync(GameUser user)
        {
            _users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
