using Game.Domain.DomainModels.Games.Entities;
using Game.Domain.DomainModels.Games.ValueObjects;
using Game.Domain.DomainModels.Rooms.Entities;
using WorldDomination.Shared.Domain;

namespace Game.Application.DTOs
{
    public sealed record CountryDto(
        Guid Id,
        string CountryName,
        string NormalizedName,
        List<RoomMemberDto> Players
    );
}