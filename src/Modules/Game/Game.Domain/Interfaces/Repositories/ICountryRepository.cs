using Game.Domain.DomainModels.Games.Entities;
using WorldDomination.Shared.Domain;

namespace Game.Domain.Interfaces.Repositories
{
    public interface ICountryRepository
    {
        Task AddAsync(Country user);
        Task DeleteAsync(Country user);
        Task<Country?> GetAsync(IdValueObject id);
        Task<Country?> GetAsync(IdValueObject id, CountryIncludes includes);
        Task UpdateAsync(Country user);   
    }

    [Flags]
    public enum CountryIncludes
    {
        Players = 0,
    }
}
