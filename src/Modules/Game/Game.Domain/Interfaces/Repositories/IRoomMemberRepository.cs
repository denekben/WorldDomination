using Game.Domain.DomainModels.Rooms.Entities;
using WorldDomination.Shared.Domain;

namespace Game.Domain.Interfaces.Repositories
{
    public interface IRoomMemberRepository
    {
        Task AddAsync(RoomMember member);
        Task DeleteAsync(RoomMember member);
        Task<RoomMember?> GetAsync(IdValueObject gameUserId, IdValueObject roomId);
        Task<RoomMember?> GetAsync(IdValueObject gameUserId, IdValueObject roomId, RoomMemberIncludes includes);
        Task UpdateAsync(RoomMember member);
    }

    [Flags]
    public enum RoomMemberIncludes
    {
        Country = 0
    }
}
