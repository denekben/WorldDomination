namespace Identity.Shared.DTOs
{
    public sealed record UserIdentityDto(Guid Id, string Username, string Token);
}
