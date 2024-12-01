using Game.Application.DTOs;
using Game.Application.Services;
using Game.Domain.DomainModels.Games.Entities;
using Game.Domain.DomainModels.Games.ValueObjects;
using Game.Domain.DomainModels.Rooms.Entities;
using Game.Infrastructure.Mappers;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics.Metrics;
using DomainGame = Game.Domain.DomainModels.Games.Entities.Game;

namespace Game.Infrastructure.Realtime
{
    internal class GameModuleNotificationService : IGameModuleNotificationService
    {
        private readonly IHubContext<GameModuleHub> _hubContext;

        public GameModuleNotificationService(IHubContext<GameModuleHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task RoomCreated(Room room)
        {
            var roomDto = room.AsRoomDto();
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

        public async Task MemberJoinedRoomForAll(RoomMember member)
        {
            var memberDto = member.AsRoomMemberDto();
            await _hubContext.Clients.All.SendAsync(nameof(MemberJoinedRoomForAll), memberDto);
        }

        public async Task MemberJoinedRoomForRoom(RoomMember member)
        {
            var memberDto = member.AsRoomMemberDto();
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

        public async Task CountryCreated(Country country, Guid roomId)
        {
            await _hubContext.Clients.Group(roomId.ToString()).SendAsync(nameof(CountryCreated), country);
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

        public async Task OrderSent(Guid countryId, Guid roomId)
        {
            await _hubContext.Clients.Group(countryId.ToString()).SendAsync(nameof(OrderSent));
        }

        public async Task GameStateChanged(string gameState, Guid roomId)
        {
            await _hubContext.Clients.Group(roomId.ToString()).SendAsync(nameof(GameStateChanged), gameState);
        }

        public async Task GameEnded(Guid roomId)
        {
            await _hubContext.Clients.Group(roomId.ToString()).SendAsync(nameof(GameEnded));
        }

        public async Task OrderChanged(OrderDto orderDto, string callerId)
        {
            await _hubContext.Clients.GroupExcept(orderDto.CountryId.ToString(), callerId).SendAsync(nameof(OrderChanged), orderDto);
        }
    }
}
