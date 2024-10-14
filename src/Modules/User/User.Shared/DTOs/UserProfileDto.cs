namespace User.Shared.DTOs
{
    public sealed record UserProfileDto(
        Guid Id,
        string? Name,
        string? Bio,
        string Username,
        string Email,
        string ProfileImagePath
    );
}
