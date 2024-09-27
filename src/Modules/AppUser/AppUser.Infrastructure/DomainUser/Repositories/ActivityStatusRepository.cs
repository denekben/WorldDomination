using AppUser.Domain.Entities;
using AppUser.Domain.Repositories;
using AppUser.Infrastructure.DomainUser.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AppUser.Infrastructure.DomainUser.Repositories
{
    public class ActivityStatusRepository : IActivityStatusRepository
    {
        private readonly DbSet<ActivityStatus> _activityStatuses;
        private readonly UserWriteDbContext _context;

        public ActivityStatusRepository(UserWriteDbContext context)
        {
            _activityStatuses = context.ActivityStatuses;
            _context = context;
        }

        public async Task AddAsync(ActivityStatus activityStatus)
        {
            await _activityStatuses.AddAsync(activityStatus);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ActivityStatus activityStatus)
        {
            _activityStatuses.Remove(activityStatus);
            await _context.SaveChangesAsync();
        }

        public async Task<ActivityStatus> GetAsync(Guid id)
        {
            return await _activityStatuses.FirstOrDefaultAsync(status => status.UserId.Value == id);
        }

        public async Task UpdateAsync(ActivityStatus activityStatus)
        {
            _activityStatuses.Update(activityStatus);
            await _context.SaveChangesAsync();
        }
    }
}
