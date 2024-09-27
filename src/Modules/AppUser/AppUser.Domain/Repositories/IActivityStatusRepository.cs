using AppUser.Domain.Entities;

namespace AppUser.Domain.Repositories
{
    public interface IActivityStatusRepository
    {
        public Task<ActivityStatus> GetAsync(Guid id);
        public Task AddAsync(ActivityStatus user);
        public Task UpdateAsync(ActivityStatus user);
        public Task DeleteAsync(Guid id);
    }
}
