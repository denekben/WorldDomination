using Game.Domain.CountryAggregate.Entities;
using WorldDomination.Shared.Domain;

namespace Game.Infrastructure.Repositories
{
    public class CityRepository : IRepository<City>
    {
        public Task AddAsync(City user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(City user)
        {
            throw new NotImplementedException();
        }

        public Task<City?> GetAsync(IdValueObject id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(City user)
        {
            throw new NotImplementedException();
        }
    }
}
