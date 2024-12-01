using FakeItEasy;
using Game.Application.Countries;
using Game.Application.Helpers;
using Game.Application.Services;
using Game.Application.UseCases.Countries.Handlers;
using Game.Domain.DomainModels.Games.Entities;
using Game.Domain.DomainModels.Rooms.Entities;
using Game.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using WorldDomination.Shared.Services;

public class JoinCountryHandlerTests
{
    private readonly JoinCountryHandler _handler;
    private readonly IHttpContextService _contextService;
    private readonly IRoomMemberRepository _roomMemberRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly ILogger<JoinCountryHandler> _logger;
    private readonly IGameModuleNotificationService _notifications;
    private readonly IGameModuleHelper _gameModuleService;

    public JoinCountryHandlerTests()
    {
        _contextService = A.Fake<IHttpContextService>();
        _roomMemberRepository = A.Fake<IRoomMemberRepository>();
        _roomRepository = A.Fake<IRoomRepository>();
        _countryRepository = A.Fake<ICountryRepository>();
        _logger = A.Fake<ILogger<JoinCountryHandler>>();
        _notifications = A.Fake<IGameModuleNotificationService>();
        _gameModuleService = A.Fake<IGameModuleHelper>();

        _handler = new JoinCountryHandler(
            _contextService,
            _roomMemberRepository,
            _roomRepository,
            _countryRepository,
            _logger,
            _notifications,
            _gameModuleService
        );
    }

    [Fact]
    public async Task Handle_ValidRequest_SuccessfullyJoinsCountry()
    {
        Arrange
       var userId = Guid.NewGuid();
        var roomId = Guid.NewGuid();
        var countryId = Guid.NewGuid();
        var command = new JoinCountry(countryId, roomId);

        var member = new RoomMember { GameUserId = userId };
        var room = new Room { Id = roomId, HasTeams = true };
        var country = new Country { Id = countryId };

        A.CallTo(() => _contextService.GetCurrentUserId()).Returns(userId);
        A.CallTo(() => _roomMemberRepository.GetAsync(userId, roomId)).Returns(member);
        A.CallTo(() => _roomRepository.GetAsync(roomId, RoomIncludes.DomainGame)).Returns(room);
        A.CallTo(() => _countryRepository.GetAsync(countryId, CountryIncludes.Players)).Returns(country);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        A.CallTo(() => _countryRepository.UpdateAsync(country)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _notifications.MemberJoinedCountry(member, roomId, countryId)).MustHaveHappenedOnceExactly();
    }

}