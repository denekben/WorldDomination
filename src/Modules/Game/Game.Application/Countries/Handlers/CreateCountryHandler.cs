using Game.Application.Services;
using Game.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using WorldDomination.Shared.Services;

namespace Game.Application.Countries.Handlers
{
    internal sealed class CreateCountryHandler : IRequestHandler<CreateCountry>
    {
        private readonly IGameModuleService _gameModuleService;
        private readonly ICountryFabric _countryFabric;
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomMemberRepository _roomMemberRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IHttpContextService _contextService;
        private readonly IGameModuleNotificationService _notifications;
        private readonly ILogger<CreateCountryHandler> _logger;


        public CreateCountryHandler(ICountryFabric countryFabric, IRoomRepository roomRepository,
            IHttpContextService contextService, IRoomMemberRepository roomMemberRepository,
            IGameModuleNotificationService notifications, ILogger<CreateCountryHandler> logger,
            IGameModuleService gameModuleService, ICountryRepository countryRepository)
        {
            _countryFabric = countryFabric;
            _roomRepository = roomRepository;
            _contextService = contextService;
            _roomMemberRepository = roomMemberRepository;
            _notifications = notifications;
            _logger = logger;
            _gameModuleService = gameModuleService;
            _countryRepository = countryRepository;
        }

        public async Task Handle(CreateCountry command, CancellationToken cancellationToken)
        {
            if (await _countryRepository.ExistsByNormalizedNameAsync(command.RoomId, command.NormalizedName))
                throw new BadRequestException($"Country with NormalizedName {command.NormalizedName} already exists");

            var userId = _contextService.GetCurrentUserId();
            var member = await _roomMemberRepository.GetAsync(userId, command.RoomId)
                ?? throw new BadRequestException($"Cannot find RoomMember {userId}");

            var room = await _roomRepository.GetAsync(command.RoomId, RoomIncludes.Countries | RoomIncludes.DomainGame)
                ?? throw new BadRequestException($"Cannot find Room {command.RoomId}");

            if (room.DomainGame != null)
                throw new BadImageFormatException($"Cannot add RoomMember {member.GameUserId} to Country when Game {room.Id} is active");

            var country = await _countryFabric.CreateCountry(command.NormalizedName, command.RoomId)
                ?? throw new BadRequestException($"Cannot create Country {command.NormalizedName} for Room {command.RoomId}");

            if (member.CountryId != null)
            {
                await _gameModuleService.RemoveMemberFromCountry(member);
            }

            room.AddCountry(country);
            country.AddPlayer(member, room.HasTeams);

            await _roomRepository.UpdateAsync(room);

            _logger.LogInformation($"Country {country.Id} created for Room {room.Id}");
            await _notifications.CountryCreated(country, command.RoomId);
        }
    }
}
