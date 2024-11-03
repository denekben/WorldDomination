using Game.Domain.DomainModels.RoomAggregate.Entities;
using WorldDomination.Shared.Domain;
using DomainGame = Game.Domain.DomainModels.GameAggregate.Entities.Game;

namespace Game.Application.Services
{
    public interface IGameModuleService
    {
        Task RemoveMemberFromCountry(RoomMember member);
    }
}
