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

        public CreateGameHanlder(ILogger<CreateGameHanlder> logger, IRepository<DomainGame> gameRepository)
        {
            _logger = logger;
            _gameRepository = gameRepository;
        }

        public async Task<Guid> Handle(CreateGame command, CancellationToken cancellationToken)
        {
            var (gameType, roomId) = command;

            var game = DomainGame.Create(gameType, roomId)
                ?? throw new BadRequestException("Cannot create Game");

            await _gameRepository.AddAsync(game);
            _logger.LogInformation($"Game {game.Id} created");

            return game.Id;
        }
    }
}
