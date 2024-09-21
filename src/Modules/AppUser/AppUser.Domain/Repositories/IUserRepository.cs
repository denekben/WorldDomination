using UserAccess.Domain.Entities;

namespace AppUser.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetAsync(Guid id);
        public Task<User> AddAsync(User user);
        public Task<User> UpdateAsync(User user);
        public Task<User> DeleteAsync(Guid id);
    }
}
