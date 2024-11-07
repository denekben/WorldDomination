using DomainGame = Game.Domain.DomainModels.Games.Entities.Game;
using WorldDomination.Shared.Domain;


namespace Game.Domain.Interfaces.Repositories
{
    public interface IGameRepository
    {
        Task AddAsync(DomainGame user);
        Task DeleteAsync(DomainGame user);
        Task<DomainGame?> GetAsync(IdValueObject roomId);
        Task<DomainGame?> GetAsync(IdValueObject roomId, GameIncludes includes);
        Task UpdateAsync(DomainGame user);
    }

    [Flags]
    public enum GameIncludes
    {
        Countries = 0,
        CountriesWithCities = 1
    }
}
