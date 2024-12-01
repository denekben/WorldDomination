namespace Identity.Application.DTOs
{
    public sealed record UserIdentityDto(Guid Id, string Username, string AccessToken, string? RefreshToken= null);
}
