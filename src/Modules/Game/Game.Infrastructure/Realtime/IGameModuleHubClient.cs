using Game.Shared.DTOs;

namespace Game.Infrastructure.Realtime
{
    public interface IGameModuleHubClient
    {
        //Room
        Task RoomCreated(RoomDto roomDto);
        Task RoomClosed(Guid roomId);
        Task RoomUpdated(RoomDto roomDto);
        Task MemberLeftRoom(RoomMemberDto memberDto, Guid roomId);
        Task MemberJoinedRoom(RoomMemberDto memberDto, Guid roomId);
        Task MemberPromotedToOrganizer(RoomMemberDto memberDto, Guid roomId);
        
        //Country
        Task CountryCreated(CountryDto countryDto, Guid roomId);
        Task MemberJoinedCountry(RoomMemberDto member, Guid roomId, Guid countryId);
        Task MinisterPromotedToPresident(RoomMemberDto member, Guid roomId, Guid countryId);
        Task MemberLeftCountry(RoomMemberDto member, Guid roomId, Guid countryId);

        //Game
        Task GameCreated(GameDto gameDto, Guid roomId);
    }
}
