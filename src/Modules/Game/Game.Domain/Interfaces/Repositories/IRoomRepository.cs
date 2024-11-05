using Game.Domain.DomainModels.Rooms.Entities;
using WorldDomination.Shared.Domain;

namespace Game.Domain.Interfaces.Repositories
{
    public interface IRoomRepository
    {
        Task AddAsync(Room room);
        Task DeleteAsync(Room room);
        Task<Room?> GetAsync(IdValueObject id, RoomIncludes includes);
        Task UpdateAsync(Room room);
    }

    [Flags]
    public enum RoomIncludes
    {
        RoomMembers = 0,
        Countries = 1,
        Creator = 2,
        DomainGame = 3
    }
}
