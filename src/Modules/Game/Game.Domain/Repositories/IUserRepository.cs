using Game.Domain.DomainModels.UserAggregate.Entities;
using WorldDomination.Shared.Domain;

namespace Game.Domain.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(GameUser user);
        Task DeleteAsync(GameUser user);
        Task<GameUser?> GetAsync(IdValueObject id);
        Task UpdateAsync(GameUser user);
    }
}
