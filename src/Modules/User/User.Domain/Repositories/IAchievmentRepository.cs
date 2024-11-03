using User.Domain.Entities;
using WorldDomination.Shared.Domain;

namespace User.Domain.Repositories
{
    public interface IAchievmentRepository
    {
        Task AddAsync(Achievment achievment);
        Task DeleteAsync(Achievment achievment);
        Task<Achievment?> GetAsync(IdValueObject id);
        Task UpdateAsync(Achievment achievment);
    }
}
