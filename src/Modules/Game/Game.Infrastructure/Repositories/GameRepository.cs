using WorldDomination.Shared.Domain;
using DomainGame = Game.Domain.GameAggregate.Entities.Game;

namespace Game.Infrastructure.Repositories
{
    public class GameRepository : IRepository<DomainGame>
    {
        public Task AddAsync(DomainGame user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(DomainGame user)
        {
            throw new NotImplementedException();
        }

        public Task<DomainGame?> GetAsync(IdValueObject id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(DomainGame user)
        {
            throw new NotImplementedException();
        }
    }
}
