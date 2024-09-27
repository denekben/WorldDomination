using AppUser.Domain.Entities;
using AppUser.Domain.Repositories;
using AppUser.Infrastructure.DomainUser.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AppUser.Infrastructure.DomainUser.Repositories
{
    public class AchievmentRepository : IAchievmentRepository
    {
        private readonly DbSet<Achievment> _achievments;
        private readonly UserWriteDbContext _context;

        public AchievmentRepository(UserWriteDbContext context)
        {
            _achievments = context.Achievments;
            _context = context;
        }

        public async Task AddAsync(Achievment achievment)
        {
            await _achievments.AddAsync(achievment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Achievment achievment)
        {
            _achievments.Remove(achievment);
            await _context.SaveChangesAsync();
        }

        public async Task<Achievment> GetAsync(Guid id)
        {
            return await _achievments.FirstOrDefaultAsync(a => a.Id.Value == id);
        }

        public async Task UpdateAsync(Achievment achievment)
        {
            _achievments.Update(achievment);
            await _context.SaveChangesAsync();
        }
    }
}
