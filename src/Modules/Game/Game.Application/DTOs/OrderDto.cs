namespace Game.Application.DTOs
{
    public sealed record OrderDto(
        Guid CountryId,
        List<Guid> CitiesToDevelop,
        List<Guid> CitiesToSetShield,
        bool DevelopEcologyProgram,
        bool DiscoverNuclearTechology,
        int BombsToBuyQuantity,
        List<Guid> CitiesToStrike,
        List<Guid> CountriesToSetSanctions,
        Guid RoomId
    );
}
