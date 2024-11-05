using Game.Application.Services;
using Game.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using DomainGame = Game.Domain.DomainModels.Games.Entities.Game;

namespace Game.Application.Games.Commands.Hanlders
{
    internal sealed class CreateGameHanlder : IRequestHandler<CreateGame, Guid>
    {
        private readonly ILogger<CreateGameHanlder> _logger;
        private readonly IRoomRepository _roomRepository;
        private readonly IGameModuleNotificationService _notifications;

        public CreateGameHanlder(ILogger<CreateGameHanlder> logger,
            IRoomRepository roomRepository, IGameModuleNotificationService notifications)
        {
            _logger = logger;
            _roomRepository = roomRepository;
            _notifications = notifications;
        }

        public async Task<Guid> Handle(CreateGame command, CancellationToken cancellationToken)
        {
            var (gameType, hasTeams, roomId) = command;

            var room = await _roomRepository.GetAsync(roomId, RoomIncludes.DomainGame)
                ?? throw new BadRequestException($"Cannot find Room {roomId}");

            if (room.DomainGame != null)
                throw new BadRequestException($"Game already created for Room {roomId}");

            var game = DomainGame.Create(gameType, hasTeams, roomId)
                ?? throw new BadRequestException("Cannot create Game");

            room.AddGame(game);

            await _roomRepository.UpdateAsync(room);
            _logger.LogInformation($"Game {game.RoomId} created");
            await _notifications.GameCreated(game, room.Id);

            return game.RoomId;
        }
    }
}
