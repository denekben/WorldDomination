using Game.Application.Services;
using Game.Domain.DomainModels.Games.Entities;
using Game.Domain.DomainModels.Rooms.Entities;
using Game.Infrastructure.Mappers;
using Microsoft.AspNetCore.SignalR;
using DomainGame = Game.Domain.DomainModels.Games.Entities.Game;

namespace Game.Infrastructure.Realtime
{
    internal class GameModuleNotificationService : IGameModuleNotificationService
    {
        private readonly IHubContext<GameModuleHub, IGameModuleHubClient> _hubContext;

        public GameModuleNotificationService(IHubContext<GameModuleHub, IGameModuleHubClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task RoomCreated(Room room)
        {
            var roomDto = room.AsRoomDto();
            await _hubContext.Clients.All.RoomCreated(roomDto);
        }

        public async Task RoomClosed(Guid roomId)
        {
            await _hubContext.Clients.All.RoomClosed(roomId);
        }

        public async Task MemberJoinedRoom(RoomMember member, Guid roomId)
        {
            var memberDto = member.AsRoomMemberDto();
            //await _hubContext.Clients.Group()'

            await Task.CompletedTask;
        }

        public async Task MemberLeftRoom(RoomMember member, Guid roomId)
        {
            var memberDto = member.AsRoomMemberDto();
            //await _hubContext.Clients.Group()

            await Task.CompletedTask;
        }

        public async Task GameCreated(DomainGame game, Guid roomId)
        {
            await Task.CompletedTask;
        }

        public async Task MemberPromotedToOrganizer(RoomMember member, Guid roomId)
        {
            await Task.CompletedTask;
        }

        public async Task CountryCreated(Country country, Guid roomId)
        {
            await Task.CompletedTask;
        }

        public async Task MemberJoinedCountry(RoomMember member, Guid roomId, Guid countryId)
        {
            await Task.CompletedTask;
        }

        public async Task MinisterPromotedToPresident(RoomMember member, Guid roomId, Guid countryId)
        {
            await Task.CompletedTask;
        }

        public async Task MemberLeftCountry(RoomMember member, Guid roomId, Guid countryId)
        {
            await Task.CompletedTask;
        }

        public async Task OrderSent(Guid countryId, Guid roomId)
        {
            await Task.CompletedTask;
        }
    }
}
