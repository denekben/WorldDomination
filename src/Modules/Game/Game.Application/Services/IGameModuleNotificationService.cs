using Game.Domain.DomainModels.Rooms.Entities;
using Game.Domain.DomainModels.Games.Entities;
using Game.Application.DTOs;

namespace Game.Application.Services
{
    public interface IGameModuleNotificationService
    {
        // Country
        Task CountryCreated(CountryDto countryDto, Guid roomId);
        Task MemberJoinedCountry(Guid memberId, Guid roomId, Guid countryId);
        Task MinisterPromotedToPresident(Guid memberId, Guid roomId, Guid countryId);
        Task MemberLeftCountry(Guid memberId, Guid roomId, Guid countryId);
        Task CountryDeleted(Guid roomId, Guid countryId);

        // Room
        Task RoomCreated(RoomDto roomDto);
        Task RoomClosedForAll(Guid roomId);
        Task RoomClosedForRoom(Guid roomId);
        Task MemberJoinedRoomForAll(RoomMemberDto memberDto);
        Task MemberJoinedRoomForRoom(RoomMemberDto memberDto);
        Task MemberLeftRoomForAll(Guid roomId, Guid memberId);
        Task MemberLeftRoomForRoom(Guid roomId, Guid memberId);
        Task MemberPromotedToOrganizer(Guid memberId, Guid roomId);

        // Game
        Task GameCreatedForAll(Guid roomId);
        Task GameCreatedForRoom(Guid roomId);
        Task GameStateChanged(string gameState, Guid roomId);
        Task GameEnded(Guid roomId);

        // Order
        Task OrderSent(Guid countryId, Guid roomId);
        Task OrderChanged(OrderDto orderDto, string callerId);

        // Donation
        Task DonationSent(CountryDto countryDto, Guid countryToDonateId, int donationValue);

        // Messaging
        Task MessageSent(RoomMemberDto memberDto, string messageText, Guid chatId);

        // Negotitation
        Task NegotiationRequestSent(Guid issuerCountryId, Guid audienceCountryId);
        Task NegotiationRequestApplied(Guid issuerCountryId, Guid audienceCountryId, Guid issuerMemberId);
        Task NegotiationTerminated(Guid firstCountryId, Guid secondCountryId);
    }
}
