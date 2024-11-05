using Game.Domain.DomainModels.Users.Entities;
using WorldDomination.Shared.Domain;

namespace Game.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(GameUser user);
        Task DeleteAsync(GameUser user);
        Task<GameUser?> GetAsync(IdValueObject id);
        Task UpdateAsync(GameUser user);
    }
}
