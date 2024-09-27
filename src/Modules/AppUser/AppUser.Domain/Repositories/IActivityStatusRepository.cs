using AppUser.Domain.Entities;

namespace AppUser.Domain.Repositories
{
    public interface IActivityStatusRepository
    {
        public Task<ActivityStatus> GetAsync(Guid id);
        public Task AddAsync(ActivityStatus activityStatus);
        public Task UpdateAsync(ActivityStatus activityStatus);
        public Task DeleteAsync(ActivityStatus activityStatus);
    }
}
