namespace AppUser.Shared.DTOs
{
    public sealed record UserProfileDto(
        Guid Id,
        string Username,
        string Email,
        string ProfileImagePath,
        string ActivityStatus,
        ICollection<UserAchievmentDto> Achievments,
        DateTime CreatedTime,
        DateTime UpdatedTime  
    );
}
