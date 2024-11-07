using Game.Application.Services;
using Game.Domain.DomainModels.Rooms.Entities;
using Game.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using WorldDomination.Shared.Services;
using DomainGame = Game.Domain.DomainModels.Games.Entities.Game;

namespace Game.Application.Games.Commands.Hanlders
{
    internal sealed class CreateGameHanlder : IRequestHandler<CreateGame, Guid>
    {
        private readonly ILogger<CreateGameHanlder> _logger;
        private readonly IRoomRepository _roomRepository;
        private readonly IGameModuleNotificationService _notifications;
        private readonly IHttpContextService _contextService;
        private readonly IRoomMemberRepository _memberRepository;

        public CreateGameHanlder(ILogger<CreateGameHanlder> logger,
            IRoomRepository roomRepository, IGameModuleNotificationService notifications,
            IHttpContextService contextService, IRoomMemberRepository memberRepository)
        {
            _logger = logger;
            _roomRepository = roomRepository;
            _notifications = notifications;
            _contextService = contextService;
            _memberRepository = memberRepository;
        }

        public async Task<Guid> Handle(CreateGame command, CancellationToken cancellationToken)
        {
            var userId = _contextService.GetCurrentUserId();
            var member = await _memberRepository.GetAsync(userId, command.RoomId)
                ?? throw new BadRequestException($"Cannot find RoomMember {userId}");

            if (member is not Organizer)
                throw new BadRequestException($"Only Organizer can create a Game");

            var (gameType, hasTeams, roundQuantity, roomId) = command;

            var room = await _roomRepository.GetAsync(roomId, RoomIncludes.RoomMembers)
                ?? throw new BadRequestException($"Cannot find Room {roomId}");

            if (room.IsGameActive)
                throw new BadImageFormatException($"Cannot create a Game for Room {room.Id} when Game is active");

            var game = DomainGame.Create(gameType, hasTeams, roundQuantity, roomId, room.RoomMembers)
                ?? throw new BadRequestException("Cannot create Game");

            room.AddGame(game);

            await _roomRepository.UpdateAsync(room);
            _logger.LogInformation($"Game {game.RoomId} created");
            await _notifications.GameCreated(game, room.Id);

            return game.RoomId;
        }
    }
}
