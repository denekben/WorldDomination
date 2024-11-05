using Game.Domain.DomainModels.Rooms.Entities;

namespace Game.Application.Helpers
{
    public interface IGameModuleHelper
    {
        Task RemoveMemberFromCountry(RoomMember member);
    }
}
