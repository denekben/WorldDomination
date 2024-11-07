using Game.Domain.DomainModels.Rooms.ValueObjects;
using Game.Domain.Interfaces.Repositories;
using MediatR;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using WorldDomination.Shared.Services;

namespace Game.Application.Orders.Commands.Handlers
{
    internal sealed class ApplyOrderHandler : IRequestHandler<ApplyOrder>
    {
        private readonly IHttpContextService _contextService;
        private readonly IRoomMemberRepository _roomMemberRepository;
        private readonly IGameRepository _gameRepository;

        public ApplyOrderHandler(IHttpContextService contextService, IRoomMemberRepository roomMemberRepository, 
            IGameRepository gameRepository)
        {
            _contextService = contextService;
            _roomMemberRepository = roomMemberRepository;
            _gameRepository = gameRepository;
        }

        public async Task Handle(ApplyOrder command, CancellationToken cancellationToken)
        {
            var order = command.Order;

            var userId = _contextService.GetCurrentUserId();
            var roomMember = await _roomMemberRepository.GetAsync(userId, order.RoomId)
                ?? throw new BadRequestException($"Cannot find Member {userId} in Room {order.RoomId}");

            var game = await _gameRepository.GetAsync(roomMember.RoomId, GameIncludes.CountriesWithCities)
                ?? throw new BadRequestException($"Cannot find Game {roomMember.RoomId}");

            var country = game.Countries.FirstOrDefault(c => c.Id == roomMember.CountryId)
                ?? throw new BadRequestException($"Cannot find Country {roomMember.CountryId}");

            country.ValidateOrder(roomMember, order, game);
        }
    }
}
