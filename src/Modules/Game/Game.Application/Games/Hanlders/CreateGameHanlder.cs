using Game.Application.Services;
using Game.Domain.RoomAggregate.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Domain;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using DomainGame = Game.Domain.GameAggregate.Entities.Game;

namespace Game.Application.Games.Hanlders
{
    internal sealed class CreateGameHanlder : IRequestHandler<CreateGame, Guid>
    {
        private readonly ILogger<CreateGameHanlder> _logger;
        private readonly IRepository<DomainGame> _gameRepository;
        private readonly IRepository<Room> _roomRepository;
        private readonly IGameService _gameService;

        public CreateGameHanlder(ILogger<CreateGameHanlder> logger, IRepository<DomainGame> gameRepository, IRepository<Room> roomRepository, IGameService gameService)
        {
            _logger = logger;
            _gameRepository = gameRepository;
            _roomRepository = roomRepository;
            _gameService = gameService;
        }

        public async Task<Guid> Handle(CreateGame command, CancellationToken cancellationToken)
        {
            var (gameType, roomId) = command;

            var room = await _roomRepository.GetAsync(roomId)
                ?? throw new BadRequestException($"Cannot find Room {roomId}");

            if (await _gameService.GetGameByRoomId(room.Id) != null)
                throw new BadRequestException($"Cannot create Game for Room {room.Id} with active game");

            var game = DomainGame.Create(gameType, roomId)
                ?? throw new BadRequestException("Cannot create Game");

            await _gameRepository.AddAsync(game);
            _logger.LogInformation($"Game {game.Id} created");

            return game.Id;
        }
    }
}
