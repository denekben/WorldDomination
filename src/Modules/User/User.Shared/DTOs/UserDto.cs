namespace User.Shared.DTOs
{
    public sealed record UserDto(
        Guid Id,
        string Name,
        string Bio,
        string Username,
        string Email,
        string ProfileImagePath,
        UserStatusDto Status,
        ICollection<UserAchievmentDto>? Achievments
    );
}
