using AppUser.Domain.Entities;

namespace AppUser.Domain.Repositories
{
    public interface IAchievmentRepository
    {
        public Task<Achievment> GetAsync(Guid id);
        public Task AddAsync(Achievment achievment);
        public Task UpdateAsync(Achievment achievment);
        public Task DeleteAsync(Achievment achievment);
    }
}
