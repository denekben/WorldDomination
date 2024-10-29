using Game.Application.Services;
using Game.Domain.DomainModels.RoomAggregate.Abstractions;
using Game.Domain.RoomAggregate.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Domain;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using WorldDomination.Shared.Services;

namespace Game.Application.Rooms.Commands.Handlers
{
    internal sealed class CloseRoomHandler : IRequestHandler<CloseRoom>
    {
        private readonly IHttpContextService _contextService;
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<RoomMember> _roomMemberRepository;
        private readonly ILogger<CloseRoomHandler> _logger;
        private readonly IGameService _gameService;

        public CloseRoomHandler(IHttpContextService contextService, IRepository<Room> roomRepository,
            IRepository<RoomMember> roomMemberRepository, ILogger<CloseRoomHandler> logger, IGameService gameService)
        {
            _contextService = contextService;
            _roomRepository = roomRepository;
            _roomMemberRepository = roomMemberRepository;
            _logger = logger;
            _gameService = gameService;
        }

        public async Task Handle(CloseRoom command, CancellationToken cancellationToken)
        {
            var userId = _contextService.GetCurrentUserId();
            var member = await _roomMemberRepository.GetAsync(userId)
                ?? throw new BadRequestException("Room meber not found");

            if (member is not Organizer)
                throw new BadRequestException("Only Organizer can close Room");

            var room = await _roomRepository.GetAsync(command.RoomId)
                ?? throw new BadRequestException("Room not found");

            if (await _gameService.GetGameByRoomId(room.Id) != null)
                throw new BadRequestException($"Cannot delete Room {room.Id} with existing Game");

            await _roomRepository.DeleteAsync(room);
            _logger.LogInformation($"Room {room.Id} deleted");
        }
    }
}