using User.Domain.Entities;
using WorldDomination.Shared.Domain;

namespace User.Domain.Repositories
{
    public interface IUserStatusRepository
    {
        public Task<UserStatus?> GetAsync(IdValueObject id);
        public Task AddAsync(UserStatus activityStatus);
        public Task UpdateAsync(UserStatus activityStatus);
        public Task DeleteAsync(UserStatus activityStatus);
    }
}
