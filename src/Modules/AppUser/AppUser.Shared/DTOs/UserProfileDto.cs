namespace AppUser.Shared.DTOs
{
    public sealed record UserProfileDto(
        Guid Id,
        string Username,
        string Email,
        string ProfileImagePath,
        ActivityStatusDto ActivityStatus,
        IEnumerable<AchievmentDto>? Achievments
    );
}
