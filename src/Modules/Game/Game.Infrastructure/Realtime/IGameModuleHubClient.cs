using Game.Shared.DTOs;

namespace Game.Infrastructure.Realtime
{
    public interface IGameModuleHubClient
    {
        //Room
        Task RoomCreated(RoomDto roomDto);
        Task RoomClosed(Guid roomId);
        Task MemberLeftRoom(RoomMemberDto memberDto, Guid roomId);
        Task MemberJoinedRoom(RoomMemberDto memberDto, Guid roomId);
        Task MemberPromotedToOrganizer(RoomMemberDto memberDto, Guid roomId);
        
        //Country
        Task CountryCreated(CountryDto countryDto);
        Task MemberJoinedCountry(RoomMemberDto member, Guid countryId);
        Task MinisterPromotedToPresident(RoomMemberDto member, Guid countryId);
        Task MemberLeftCountry(RoomMemberDto member, Guid countryId);

        //Game
        Task GameCreated(GameDto gameDto, Guid roomId);

        //Order
        Task OrderChanged(OrderDto orderDto);
        Task OrderApplied();
    }
}
