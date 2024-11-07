namespace Game.Shared.DTOs
{
    public sealed record OrderDto(
        List<Guid> CitiesIdToDevelop,
        List<Guid> CitiesToSetShield,
        bool DevelopEcologyProgram,
        bool DiscoverNuclearTechology,
        uint BombsToBuyQuantity,
        List<Guid> CitiesToStrike,
        List<Guid> CountriesToSetSanctions,
        Guid CountryId,
        Guid RoomId
    );
}
