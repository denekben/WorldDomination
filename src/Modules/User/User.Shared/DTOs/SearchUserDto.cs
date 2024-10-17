namespace User.Shared.DTOs
{
    public record SearchUserDto(
        Guid Id,
        string Username,
        string ProfileImagePath
    );
}
