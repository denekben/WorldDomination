using User.Infrastructure.ReadModels;
using User.Shared.DTOs;

namespace User.Infrastructure.Queries
{
    public static class Extensions
    {
        public static UserProfileDto AsDto(this UserReadModel user)
        {
            return new UserProfileDto
            (
                user.Id,
                user.Name,
                user.Bio,
                user.Username,
                user.Email,
                user.ProfileImagePath
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
                status.UserId,
                status.ActivityStatus,
                status.Country,
                status.RoundNumber,
                status.GameRole
            );
        }
    }
}