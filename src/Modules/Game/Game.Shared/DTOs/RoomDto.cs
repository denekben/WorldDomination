namespace Game.Shared.DTOs
{
    public sealed record RoomDto(
        Guid Id,
        string RoomName,
        string GameType,
        int CountryQuantity,
        bool IsPrivate,
        DateTime CreatedTime,
        List<RoomMemberDto> RoomMembers
    );
}
