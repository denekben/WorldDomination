using Game.Application.Services;
using Game.Application.UseCases.Games.Commands;
using Game.Domain.DomainModels.Games.ValueObjects;
using Game.Domain.DomainModels.Rooms.Entities;
using Game.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Application.UseCases.Games.Commands.Hanlders
{
    internal sealed class ChangeGameStateHandler : IRequestHandler<ChangeGameState>
    {
        private readonly IRoomMemberRepository _roomMemberRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IGameModuleNotificationService _notifications;
        private readonly ILogger<ChangeGameStateHandler> _logger;

        public ChangeGameStateHandler(IRoomMemberRepository roomMemberRepository,
            IGameRepository gameRepository, IGameModuleNotificationService notifications,
            ILogger<ChangeGameStateHandler> logger)
        {
            _roomMemberRepository = roomMemberRepository;
            _gameRepository = gameRepository;
            _notifications = notifications;
            _logger = logger;
        }

        public async Task Handle(ChangeGameState command, CancellationToken cancellationToken)
        {
            var member = await _roomMemberRepository.GetAsync(command.CallerId, command.RoomId)
                ?? throw new BadRequestException($"Cannot find RoomMember {command.CallerId}");

            if (member is not Organizer)
                throw new BusinessRuleValidationException("Only Organizer can change GameState");

            var game = await _gameRepository.GetAsync(command.GameId, GameIncludes.CountriesWithCitiesWithOrders)
                ?? throw new BadRequestException($"Cannot find Game {command.GameId}");

            if (game.HasGameStateTimer)
                throw new BusinessRuleValidationException($"Game with GameStateTimer changes states automatically");

            game.ChangeState();

            if (game.GameState == GameState.Debates)
            {
                foreach (var country in game.Countries)
                {
                    if (country.HasValidatedOrder)
                        country.ApplyOrder(country.Order, game.Countries, game);
                }

                foreach (var country in game.Countries)
                {
                    country.UpdateState(game.EcologyLevel);
                }
            }
            await _gameRepository.UpdateAsync(game);

            if (game.CurrentRound > game.RoundQuantity)
            {
                await _notifications.GameEnded(game.RoomId);
                _logger.LogInformation($"Game {game.RoomId} ended");
                return;
            }

            _logger.LogInformation($"Game {game.RoomId} changed state to {game.GameState}");
            await _notifications.GameStateChanged(game.GameState, game.RoomId);
        }
    }
}
