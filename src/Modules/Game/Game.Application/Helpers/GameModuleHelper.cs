using Game.Application.Services;
using Game.Domain.DomainModels.Rooms.Entities;
using Game.Domain.DomainModels.Rooms.ValueObjects;
using Game.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Game.Application.Helpers
{
    public class GameModuleHelper : IGameModuleHelper
    {
        private readonly ILogger<GameModuleHelper> _logger;
        private readonly IGameModuleNotificationService _notifications;
        private readonly ICountryRepository _countryRepository;

        public GameModuleHelper(ILogger<GameModuleHelper> logger,
            IGameModuleNotificationService notifications, ICountryRepository countryRepository)
        {
            _logger = logger;
            _notifications = notifications;
            _countryRepository = countryRepository;
        }

        public async Task RemoveMemberFromCountry(RoomMember member)
        {
            var latestCountry = await _countryRepository.GetAsync(member.CountryId, CountryIncludes.Players);
            latestCountry.RemovePlayer(member);
            _logger.LogInformation($"Member {member.GameUserId} left Country {member.CountryId}");

            if (latestCountry.Players.Count() != 0)
            {
                if (member.GameRole == GameRole.President)
                {
                    var newPresident = latestCountry.ElectNewPresident();
                    _logger.LogInformation($"Minister {newPresident.GameUserId} promoted to President");
                }
                await _countryRepository.UpdateAsync(latestCountry);

                if (member.GameRole == GameRole.President)
                {
                    await _notifications.MinisterPromotedToPresident(member.GameUserId, latestCountry.RoomId, latestCountry.Id);
                }

                await _notifications.MemberLeftCountry(member.GameUserId, latestCountry.RoomId, latestCountry.Id);
            }
            else
            {
                await _countryRepository.DeleteAsync(latestCountry);
                await _notifications.CountryDeleted(member.RoomId, member.CountryId);
            }
        }
    }
}
