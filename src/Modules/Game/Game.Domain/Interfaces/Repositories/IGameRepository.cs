using DomainGame = Game.Domain.DomainModels.Games.Entities.Game;
using WorldDomination.Shared.Domain;


namespace Game.Domain.Interfaces.Repositories
{
    public interface IGameRepository
    {
        Task AddAsync(DomainGame user);
        Task DeleteAsync(DomainGame user);
        Task<DomainGame?> GetAsync(IdValueObject id);
        Task<DomainGame?> GetAsync(IdValueObject firstId, IdValueObject? secondId);
        Task UpdateAsync(DomainGame user);
    }
}
