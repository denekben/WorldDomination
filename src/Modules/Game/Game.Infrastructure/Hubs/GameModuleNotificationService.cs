using Game.Application.DTOs;
using Game.Application.Services;
using Microsoft.AspNetCore.SignalR;

namespace Game.Infrastructure.Hubs
{
    internal class GameModuleNotificationService : IGameModuleNotificationService
    {
        private readonly IHubContext<GameModuleHub> _hubContext;

        public GameModuleNotificationService(IHubContext<GameModuleHub> hubContext)
        {
            _hubContext = hubContext;
        }

        // Room
        public async Task RoomCreated(RoomDto roomDto)
        {
            await _hubContext.Clients.All.SendAsync(nameof(RoomCreated), roomDto);
        }

        public async Task RoomClosedForAll(Guid roomId)
        {
            await _hubContext.Clients.All.SendAsync(nameof(RoomClosedForAll), roomId);
        }

        public async Task RoomClosedForRoom(Guid roomId)
        {
            await _hubContext.Clients.Group(roomId.ToString()).SendAsync(nameof(RoomClosedForRoom), roomId);
        }

        public async Task MemberJoinedRoomForAll(RoomMemberDto memberDto)
        {
            await _hubContext.Clients.All.SendAsync(nameof(MemberJoinedRoomForAll), memberDto);
        }

        public async Task MemberJoinedRoomForRoom(RoomMemberDto memberDto)
        {
            await _hubContext.Clients.Group(memberDto.RoomId.ToString()).SendAsync(nameof(MemberJoinedRoomForRoom), memberDto);
        }

        public async Task MemberLeftRoomForAll(Guid roomId, Guid memberId)
        {
            await _hubContext.Clients.All.SendAsync(nameof(MemberLeftRoomForAll), (roomId, memberId));
        }

        public async Task MemberLeftRoomForRoom(Guid roomId, Guid memberId)
        {
            await _hubContext.Clients.Group(roomId.ToString()).SendAsync(nameof(MemberLeftRoomForRoom), memberId);
        }

        // Game
        public async Task GameCreatedForAll(Guid roomId)
        {
            await _hubContext.Clients.All.SendAsync(nameof(GameCreatedForAll), roomId);
        }

        public async Task GameCreatedForRoom(Guid roomId)
        { 
            await _hubContext.Clients.Group(roomId.ToString()).SendAsync(nameof(GameCreatedForRoom));
        }

        public async Task MemberPromotedToOrganizer(Guid memberId, Guid roomId)
        {
            await _hubContext.Clients.Group(roomId.ToString()).SendAsync(nameof(MemberPromotedToOrganizer), memberId);
        }

        // Country
        public async Task CountryCreated(CountryDto countryDto, Guid roomId)
        {
            await _hubContext.Clients.Group(roomId.ToString()).SendAsync(nameof(CountryCreated), countryDto);
        }

        public async Task MemberJoinedCountry(Guid memberId, Guid roomId, Guid countryId)
        {
            await _hubContext.Clients.Group(roomId.ToString()).SendAsync(nameof(MemberJoinedCountry), (memberId, countryId));
        }

        public async Task MinisterPromotedToPresident(Guid memberId, Guid roomId, Guid countryId)
        {
            await _hubContext.Clients.Group(roomId.ToString()).SendAsync(nameof(MinisterPromotedToPresident), (memberId, countryId));
        }

        public async Task MemberLeftCountry(Guid memberId, Guid roomId, Guid countryId)
        {
            await _hubContext.Clients.Group(roomId.ToString()).SendAsync(nameof(MemberLeftCountry), (memberId, countryId));
        }

        public Task CountryDeleted(Guid roomId, Guid countryId)
        {
            throw new NotImplementedException();
        }

        // Order
        public async Task OrderSent(Guid countryId, Guid roomId)
        {
            await _hubContext.Clients.Group(countryId.ToString()).SendAsync(nameof(OrderSent));
        }

        public async Task OrderChanged(OrderDto orderDto, string callerId)
        {
            await _hubContext.Clients.GroupExcept(orderDto.CountryId.ToString(), callerId).SendAsync(nameof(OrderChanged), orderDto);
        }

        // Donations
        public Task DonationSent(CountryDto countryDto, Guid countryToDonateId, int donationValue)
        {
            throw new NotImplementedException();
        }

        // Game
        public async Task GameStateChanged(string gameState, Guid roomId)
        {
            await _hubContext.Clients.Group(roomId.ToString()).SendAsync(nameof(GameStateChanged), gameState);
        }

        public async Task GameEnded(Guid roomId)
        {
            await _hubContext.Clients.Group(roomId.ToString()).SendAsync(nameof(GameEnded));
        }

        // Messages
        public Task MessageSent(RoomMemberDto memberDto, string messageText, Guid chatId)
        {
            throw new NotImplementedException();
        }

        public Task NegotiationRequestSent(Guid issuerCountryId, Guid audienceCountryId)
        {
            throw new NotImplementedException();
        }

        public Task NegotiationRequestApplied(Guid issuerCountryId, Guid audienceCountryId, Guid issuerMemberId)
        {
            throw new NotImplementedException();
        }

        public Task NegotiationTerminated(Guid firstCountryId, Guid secondCountryId)
        {
            throw new NotImplementedException();
        }

        public Task CountryGotEvent(GameEventDto gameEvent, Guid countryId)
        {
            throw new NotImplementedException();
        }
    }
}
