using Game.Domain.DomainModels.Games.Entities;
using Game.Domain.DomainModels.Rooms.ValueObjects;

namespace Game.Domain.Interfaces.Countries
{
    public interface ICountryFactory
    {
        Task<Country> CreateCountry(string normalizedName, Guid roomId, GameType gameType);
    }
}
