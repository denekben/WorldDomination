namespace AppUser.Shared.DTOs
{
    public sealed record ActivityStatusDto(
        string IsInGame,
        string? Country,
        int? RoundNumber,
        string? GameRole
    );
}
