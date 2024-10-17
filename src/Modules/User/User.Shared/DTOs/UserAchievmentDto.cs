namespace User.Shared.DTOs
{
    public sealed record UserAchievmentDto(
        Guid Id,
        string AchievmentName,
        string Description,
        DateTime AchievedTime
    );
}
