using Game.Domain.CountryAggregate.Entities;
using WorldDomination.Shared.Domain;

namespace Game.Infrastructure.Repositories
{
    public class CountryRepository : IRepository<Country>
    {
        public Task AddAsync(Country user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Country user)
        {
            throw new NotImplementedException();
        }

        public Task<Country?> GetAsync(IdValueObject id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Country user)
        {
            throw new NotImplementedException();
        }
    }
}
