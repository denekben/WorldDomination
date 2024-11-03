using User.Domain.Entities;
using WorldDomination.Shared.Domain;

namespace User.Domain.Repositories
{
    public interface IUserStatusRepository
    {
        Task AddAsync(UserStatus userStatus);
        Task DeleteAsync(UserStatus userStatus);
        Task<UserStatus?> GetAsync(IdValueObject id);
        Task UpdateAsync(UserStatus userStatus);
    }
}
