using User.Domain.Entities;
using WorldDomination.Shared.Domain;

namespace User.Domain.Repositories
{
    public interface IAchievmentRepository
    {
        public Task<Achievment?> GetAsync(IdValueObject id);
        public Task AddAsync(Achievment achievment);
        public Task UpdateAsync(Achievment achievment);
        public Task DeleteAsync(Achievment achievment);
    }
}
