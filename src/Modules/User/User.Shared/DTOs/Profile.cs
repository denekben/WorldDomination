namespace User.Shared.DTOs
{
    public sealed record Profile(
        UserDto User,
        List<UserAchievmentDto>? Achievments
    );
}
