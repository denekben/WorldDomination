﻿using Game.Application.Services;
using Game.Domain.DomainModels.Games.ValueObjects;
using Game.Domain.DomainModels.Rooms.Entities;
using Game.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Application.Games.Commands.Hanlders
{
    internal sealed class ChangeGameStateHandler : IRequestHandler<ChangeGameState>
    {
        private readonly IRoomMemberRepository _roomMemberRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IGameModuleNotificationService _notifications;
        private readonly ILogger<ChangeGameStateHandler> _logger;
        private readonly IOrderRepository _orderRepository;

        public ChangeGameStateHandler(IRoomMemberRepository roomMemberRepository,
            IGameRepository gameRepository, IGameModuleNotificationService notifications,
            ILogger<ChangeGameStateHandler> logger, IOrderRepository orderRepository)
        {
            _roomMemberRepository = roomMemberRepository;
            _gameRepository = gameRepository;
            _notifications = notifications;
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public async Task Handle(ChangeGameState command, CancellationToken cancellationToken)
        {
            var member = await _roomMemberRepository.GetAsync(command.MemberId, command.RoomId)
                ?? throw new BadRequestException($"Cannot find RoomMember {command.MemberId}");

            if (member is not Organizer)
                throw new BusinessRuleValidationException("Only Organizer can change GameState");

            var game = await _gameRepository.GetAsync(command.GameId, GameIncludes.CountriesWithCitiesWithOrders)
                ?? throw new BadRequestException($"Cannot find Game {command.GameId}");

            if (game.HasGameStateTimer)
                throw new BusinessRuleValidationException($"Game with GameStateTimer changes states automatically");

            game.ChangeState();
            _logger.LogInformation($"Game {game.RoomId} changed state to {game.GameState}");

            if (game.GameState == GameState.Debates)
            {
                foreach(var country in game.Countries)
                {
                    country.ApplyOrder(country.Order, game.Countries, game);
                    country.UpdateIncome(game.EcologyLevel);
                }
            }

            await _notifications.GameStateChanged(game.GameState, game.RoomId);
        }
    }
}
