using Game.Domain.DomainModels.RoomAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using WorldDomination.Shared.Domain;

namespace Game.Domain.Repositories
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
