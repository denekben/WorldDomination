using Game.Application.Helpers;
using Game.Application.Services;
using Game.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using WorldDomination.Shared.Services;

namespace Game.Application.Countries.Handlers
{
    public sealed class JoinCountryHandler : IRequestHandler<JoinCountry>
    {
        private readonly IHttpContextService _contextService;
        private readonly IRoomMemberRepository _roomMemberRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ILogger<JoinCountryHandler> _logger;
        private readonly IGameModuleNotificationService _notifications;
        private readonly IGameModuleHelper _gameModuleService;

        public JoinCountryHandler(IHttpContextService contextService, IRoomMemberRepository roomMemberRepository,
            IRoomRepository roomRepository, ICountryRepository countryRepository, ILogger<JoinCountryHandler> logger,
            IGameModuleNotificationService notifications, IGameModuleHelper gameModuleService)
        {
            _contextService = contextService;
            _roomMemberRepository = roomMemberRepository;
            _roomRepository = roomRepository;
            _countryRepository = countryRepository;
            _logger = logger;
            _notifications = notifications;
            _gameModuleService = gameModuleService;
        }

        public async Task Handle(JoinCountry command, CancellationToken cancellationToken)
        {
            var userId = _contextService.GetCurrentUserId();
            var member = await _roomMemberRepository.GetAsync(userId, command.RoomId)
                ?? throw new BadRequestException($"Cannot find RoomMember {userId}");

            if (member.CountryId == command.CountryId)
                throw new BadRequestException($"Cannot join to the same Country");

            var room = await _roomRepository.GetAsync(command.RoomId)
                ?? throw new BadRequestException($"Cannot find Room {command.RoomId}");

            if (!room.HasTeams)
                throw new BadRequestException($"Cannot add second Member in Country when Room {room.Id} created without teams");

            if (room.IsGameActive)
                throw new BadImageFormatException($"Cannot add RoomMember {member.GameUserId} to Country when Game in Room {room.Id} is active");

            var country = await _countryRepository.GetAsync(command.CountryId, CountryIncludes.Players)
                ?? throw new BadRequestException($"Cannot find Country {command.CountryId}");

            if(member.CountryId != null)
                await _gameModuleService.RemoveMemberFromCountry(member);

            country.AddPlayer(member, room.HasTeams);
            await _countryRepository.UpdateAsync(country);

            _logger.LogInformation($"Member {member.GameUserId} joined Country {country.Id}");
            await _notifications.MemberJoinedCountry(member, command.RoomId, country.Id);
        }
    }
}
