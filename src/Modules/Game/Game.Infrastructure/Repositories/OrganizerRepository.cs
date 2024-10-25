using Game.Domain.RoomAggregate.Entities;
using WorldDomination.Shared.Domain;

namespace Game.Infrastructure.Repositories
{
    internal class OrganizerRepository : IRepository<Organizer>
    {
        public Task AddAsync(Organizer user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Organizer user)
        {
            throw new NotImplementedException();
        }

        public Task<Organizer?> GetAsync(IdValueObject id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Organizer user)
        {
            throw new NotImplementedException();
        }
    }
}
