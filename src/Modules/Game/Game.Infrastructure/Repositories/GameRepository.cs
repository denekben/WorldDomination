using Game.Domain.Interfaces.Repositories;
using WorldDomination.Shared.Domain;
using DomainGame = Game.Domain.DomainModels.Games.Entities.Game;

namespace Game.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
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

        public Task<DomainGame?> GetAsync(IdValueObject firstId, IdValueObject? secondId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(DomainGame user)
        {
            throw new NotImplementedException();
        }
    }
}
