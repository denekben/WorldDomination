using Microsoft.EntityFrameworkCore;
using User.Domain.Entities;
using WorldDomination.Shared.Domain;

namespace User.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<DomainUser?> GetAsync(IdValueObject id);
        Task AddAsync(DomainUser user);
        Task UpdateAsync(DomainUser user);
        Task DeleteAsync(DomainUser user);
    }
}
