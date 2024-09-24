using UserAccess.Domain.Entities;

namespace AppUser.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetAsync(Guid id);
        public Task AddAsync(User user);
        public Task UpdateAsync(User user);
        public Task DeleteAsync(Guid id);
    }
}
