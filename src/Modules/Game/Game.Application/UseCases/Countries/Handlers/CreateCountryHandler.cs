using Game.Application.DTOs.Mappers;
using Game.Application.Helpers;
using Game.Application.Services;
using Game.Application.UseCases.Countries;
using Game.Domain.Interfaces.Countries;
using Game.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using WorldDomination.Shared.Services;

namespace Game.Application.UseCases.Countries.Handlers
{
    internal sealed class CreateCountryHandler : IRequestHandler<CreateCountry, Guid>
    {
        private readonly IGameModuleHelper _helper;
        private readonly IGameModuleReadService _readService;
        private readonly ICountryFactory _countryFactory;
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomMemberRepository _roomMemberRepository;
        private readonly IGameModuleNotificationService _notifications;
        private readonly ILogger<CreateCountryHandler> _logger;

        public CreateCountryHandler(ICountryFactory countryFactory, IRoomRepository roomRepository,
            IRoomMemberRepository roomMemberRepository,
            IGameModuleNotificationService notifications, ILogger<CreateCountryHandler> logger,
            IGameModuleHelper helper, IGameModuleReadService readService)
        {
            _countryFactory = countryFactory;
            _roomRepository = roomRepository;
            _roomMemberRepository = roomMemberRepository;
            _notifications = notifications;
            _logger = logger;
            _helper = helper;
            _readService = readService;
        }

        public async Task<Guid> Handle(CreateCountry command, CancellationToken cancellationToken)
        {
            if (!await _readService.CountryExistsByNormalizedNameAsync(command.RoomId, command.NormalizedName))
                throw new BadRequestException($"Country with NormalizedName {command.NormalizedName} already exists");

            var userId = command.CallerId;
            var member = await _roomMemberRepository.GetAsync(userId, command.RoomId)
                ?? throw new BadRequestException($"Cannot find RoomMember {userId}");

            var room = await _roomRepository.GetAsync(command.RoomId, RoomIncludes.Countries)
                ?? throw new BadRequestException($"Cannot find Room {command.RoomId}");

            if (room.IsGameActive)
                throw new BadImageFormatException($"Cannot add RoomMember {member.GameUserId} to Country when Game {room.Id} is active");

            var country = await _countryFactory.CreateCountry(command.NormalizedName, command.RoomId, room.GameType)
                ?? throw new BadRequestException($"Cannot create Country {command.NormalizedName} for Room {command.RoomId}");

            if (member.CountryId != null)
                await _helper.RemoveMemberFromCountry(member);

            room.AddCountry(country);
            country.AddPlayer(member, room.HasTeams);

            await _roomRepository.UpdateAsync(room);

            _logger.LogInformation($"Country {country.Id} created for Room {room.Id}");
            await _notifications.CountryCreated(country.AsCountryDto(), command.RoomId);

            return country.Id;
        }
    }
}
