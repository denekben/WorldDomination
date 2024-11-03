using Game.Domain.DomainModels.GameAggregate.Entities;
using Game.Domain.DomainModels.RoomAggregate.Entities;
using WorldDomination.Shared.Domain;

namespace Game.Domain.Repositories
{
    public interface ICountryRepository
    {
        public Task AddAsync(Country user);

        public Task DeleteAsync(Country user);

        public Task<Country?> GetAsync(IdValueObject id);

        public Task<Country?> GetAsync(IdValueObject id, CountryIncludes includes);

        public Task UpdateAsync(Country user);
        
        public Task<bool> ExistsByNormalizedNameAsync(IdValueObject roomId, string normalizedName);
    }

    [Flags]
    public enum CountryIncludes
    {
        Players = 0
    }
}
