using System.Threading.Tasks;

namespace WorldDomination.Shared.Domain
{
    public interface IRepository<T>
    {
        public Task<T?> GetAsync(IdValueObject id);
        public Task AddAsync(T user);
        public Task UpdateAsync(T user);
        public Task DeleteAsync(T user);
    }
}
