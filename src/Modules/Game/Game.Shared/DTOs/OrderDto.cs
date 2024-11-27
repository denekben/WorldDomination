namespace Game.Shared.DTOs
{
    public sealed record OrderDto(
        Guid CountryId,
        List<Guid> CitiesIdToDevelop,
        List<Guid> CitiesToSetShield,
        bool DevelopEcologyProgram,
        bool DiscoverNuclearTechology,
        int BombsToBuyQuantity,
        List<Guid> CitiesToStrike,
        List<Guid> CountriesToSetSanctions,
        Guid RoomId
    );
}
