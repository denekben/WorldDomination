using Game.Domain.RoomAggregate.Entities;
using WorldDomination.Shared.Domain;

namespace Game.Infrastructure.Repositories
{
    public class PlayerRepository : IRepository<Player>
    {
        public Task AddAsync(Player user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Player user)
        {
            throw new NotImplementedException();
        }

        public Task<Player?> GetAsync(IdValueObject id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Player user)
        {
            throw new NotImplementedException();
        }
    }
}
