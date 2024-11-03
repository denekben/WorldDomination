using DomainGame = Game.Domain.DomainModels.GameAggregate.Entities.Game;
using WorldDomination.Shared.Domain;


namespace Game.Domain.Repositories
{
    public interface IGameRepository
    {
        public Task AddAsync(DomainGame user);

        public Task DeleteAsync(DomainGame user);

        public Task<DomainGame?> GetAsync(IdValueObject id);

        public Task<DomainGame?> GetAsync(IdValueObject firstId, IdValueObject? secondId);

        public Task UpdateAsync(DomainGame user);
    }
}
