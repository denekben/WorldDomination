namespace User.Shared.DTOs
{
    public sealed record AchievmentDto (
        Guid Id,
        string AchievmentName,
        string Description
    );
}
