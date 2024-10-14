using User.Domain.Entities;
using WorldDomination.Shared.Domain;

namespace User.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<DomainUser?> GetAsync(IdValueObject id);
        public Task AddAsync(DomainUser user);
        public Task UpdateAsync(DomainUser user);
        public Task DeleteAsync(DomainUser user);
    }
}
