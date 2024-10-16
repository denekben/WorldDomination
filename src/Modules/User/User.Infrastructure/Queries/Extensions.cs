using User.Infrastructure.ReadModels;
using User.Shared.DTOs;

namespace User.Infrastructure.Queries
{
    public static class Extensions
    {
        public static UserDto AsDto(this UserReadModel user)
        {
            return new UserDto(
                user.Id,
                user.Name,
                user.Bio,
                user.Username,
                user.Email,
                user.ProfileImagePath,
                user.UserStatusReadModel.AsDto(),
                user.UserAchievmentsReadModel?.Select(ua => ua.AsDto()).ToList()
            );
        }

        public static AchievmentDto AsDto(this AchievmentReadModel achievment)
        {
            return new AchievmentDto(
                achievment.Id,
                achievment.Name,
                achievment.Description
            );
        }

        public static UserStatusDto AsDto(this UserStatusReadModel status)
        {
            return new UserStatusDto(
                status.ActivityStatus,
                status.Country,
                status.RoundNumber,
                status.GameRole
            );
        }

        public static UserAchievmentDto AsDto(this UserAchievmentReadModel userAchievment)
        {
            return new UserAchievmentDto(
                userAchievment.AchievmentReadModel.AsDto(),
                userAchievment.AchievedTime
            );
        }
    }
}