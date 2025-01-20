using Game.Application.Services;
using Game.Application.UseCases.Orders.Commands;
using Game.Domain.DomainModels.Games.Entities;
using Game.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using WorldDomination.Shared.Services;

namespace Game.Application.UseCases.Orders.Commands.Handlers
{
    internal sealed class SendOrderHandler : IRequestHandler<SendOrder>
    {
        private readonly IRoomMemberRepository _roomMemberRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<SendOrderHandler> _logger;
        private readonly IGameModuleNotificationService _notifications;

        public SendOrderHandler(IRoomMemberRepository roomMemberRepository, IGameRepository gameRepository, 
            IOrderRepository orderRepository, ILogger<SendOrderHandler> logger,
            IGameModuleNotificationService notifications)
        {
            _roomMemberRepository = roomMemberRepository;
            _gameRepository = gameRepository;
            _orderRepository = orderRepository;
            _logger = logger;
            _notifications = notifications;
        }

        public async Task Handle(SendOrder command, CancellationToken cancellationToken)
        {
            var cOrder = command.Order;
            var order = Order.Create(
                cOrder.CountryId,
                cOrder.CitiesToDevelop,
                cOrder.CitiesToSetShield,
                cOrder.DevelopEcologyProgram,
                cOrder.DiscoverNuclearTechology,
                cOrder.BombsToBuyQuantity,
                cOrder.CitiesToStrike,
                cOrder.CountriesToSetSanctions,
                cOrder.CountriesToDonate,
                cOrder.RoomId) ??
                throw new BadRequestException("Cannot create order");

            var userId = command.CallerId;
            var roomMember = await _roomMemberRepository.GetAsync(userId, order.RoomId)
                ?? throw new BadRequestException($"Cannot find Member {userId} in Room {order.RoomId}");

            var game = await _gameRepository.GetAsync(roomMember.RoomId, GameIncludes.CountriesWithCities)
                ?? throw new BadRequestException($"Cannot find Game {roomMember.RoomId}");

            var country = game.Countries.FirstOrDefault(c => c.Id == roomMember.CountryId)
                ?? throw new BadRequestException($"Cannot find Country {roomMember.CountryId}");

            country.ValidateOrder(roomMember, order, game);
            _logger.LogInformation($"Order for Country {order.CountryId} was successfully validated");

            await _orderRepository.AddOrUpdateIfExistsAsync(order);
            _logger.LogInformation($"Order for Country {order.CountryId} created");

            await _notifications.OrderSent(order.CountryId, order.RoomId);
        }
    }
}
