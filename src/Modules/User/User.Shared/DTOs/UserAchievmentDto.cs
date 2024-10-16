namespace User.Shared.DTOs
{
    public sealed record UserAchievmentDto(
        AchievmentDto Achievment,
        DateTime AchievedTime
    );
}
