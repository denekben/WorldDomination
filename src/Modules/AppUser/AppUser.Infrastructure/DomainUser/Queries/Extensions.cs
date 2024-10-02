using AppUser.Infrastructure.DomainUser.ReadModels;
using AppUser.Shared.DTOs;
using System.Diagnostics.Metrics;

namespace AppUser.Infrastructure.DomainUser.Queries
{
    public static class Extensions
    {
        public static UserProfileDto AsDto(this UserReadModel user)
        {
            return new UserProfileDto
            (
                user.Id,
                user.Username,
                user.Email,
                user.ProfileImagePath,
                user.ActivityStatus.AsDto(),
                user.UserAchievments?.Select(ua => ua.AchievmentReadModel.AsDto()).ToList()
            );
        }

        public static AchievmentDto AsDto(this AchievmentReadModel achievment)
        {
            return new AchievmentDto(
                achievment.Name,
                achievment.Description
            );
        }

        public static ActivityStatusDto AsDto(this ActivityStatusReadModel status)
        {
            return new ActivityStatusDto(
                status.IsInGameStatus,
                status.Country,
                status.RoundNumber,
                status.GameRole
            );
        }
    }
}