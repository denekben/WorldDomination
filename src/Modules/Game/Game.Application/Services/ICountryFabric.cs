using Game.Domain.DomainModels.GameAggregate.Entities;

namespace Game.Application.Services
{
    public interface ICountryFabric
    {
        Task<Country> CreateCountry(string normalizedName, Guid roomId);
    }
}
