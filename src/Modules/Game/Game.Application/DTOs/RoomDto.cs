namespace Game.Application.DTOs
{
    public sealed record RoomDto(
        Guid Id,
        string RoomName,
        string GameType,
        bool IsPrivate,
        DateTime CreatedTime,
        List<RoomMemberDto> RoomMembers
    );
}
