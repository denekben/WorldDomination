using Game.Application.Services;
using Game.Domain.DomainModels.RoomAggregate.Abstractions;
using Game.Domain.RoomAggregate.Entities;
using Game.Infrastructure.Mappers;
using Microsoft.AspNetCore.SignalR;
using DomainGame = Game.Domain.GameAggregate.Entities.Game;

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
            await _hubContext.Clients.All.ReceiveRoomCreatedMessage(roomDto);
        }

        public async Task RoomClosed(Guid roomId)
        {
            await _hubContext.Clients.All.ReceiveRoomClosedMessage(roomId);
        }

        public async Task RoomUpdated(Room room)
        {
            var roomDto = room.AsRoomDto();
            await _hubContext.Clients.All.ReceiveRoomUpdatedMessage(roomDto);
        }

        public async Task MemberJoinedRoom(RoomMember member, Guid roomId)
        {
            var memberDto = member.AsRoomMemberDto();
            //await _hubContext.Clients.Group()
        }

        public async Task MemberLeftRoom(RoomMember member, Guid roomId)
        {
            var memberDto = member.AsRoomMemberDto();
            //await _hubContext.Clients.Group()
        }

        public Task GameCreated(DomainGame game)
        {
            throw new NotImplementedException();
        }
    }
}
