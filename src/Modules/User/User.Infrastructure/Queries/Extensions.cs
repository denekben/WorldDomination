using User.Infrastructure.ReadModels;
using User.Shared.DTOs;

namespace User.Infrastructure.Queries
{
    public static class Extensions
    {
        public static UserDto AsUserDto(this UserReadModel user)
        {
            return new UserDto(
                user.Id,
                user.Name,
                user.Bio,
                user.Username,
                user.Email,
                user.ProfileImagePath
            );
        }

        public static AchievmentDto AsAchievmentDto(this AchievmentReadModel achievment)
        {
            return new AchievmentDto(
                achievment.Id,
                achievment.Name,
                achievment.Description
            );
        }

        public static UserStatusDto AsUserStatusDto(this UserStatusReadModel status)
        {
            return new UserStatusDto(
                status.ActivityStatus,
                status.Country,
                status.RoundNumber,
                status.GameRole
            );
        }

        public static UserAchievmentDto AsUserAchievmentDto(this UserAchievmentReadModel userAchievment)
        {
            var achievment = userAchievment.Achievment.AsAchievmentDto();

            return new UserAchievmentDto(
                achievment.Id,
                achievment.AchievmentName,
                achievment.Description,
                userAchievment.AchievedTime
            );
        }

        public static SearchUserDto AsSearchUserDto(this UserReadModel user)
        {
            return new SearchUserDto(
                user.Id,
                user.Username,
                user.ProfileImagePath
            );
        }
    }
}