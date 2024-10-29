using Game.Domain.DomainModels.RoomAggregate.Abstractions;
using WorldDomination.Shared.Domain;
using DomainGame = Game.Domain.GameAggregate.Entities.Game;

namespace Game.Application.Services
{
    public interface IGameModuleService
    {
        Task<DomainGame?> GetGameByRoomId(IdValueObject roomId);
        Task<List<RoomMember>?> GetRoomMembersByRoomId(IdValueObject roomId);
    }
}
