namespace Game.Shared.DTOs
{
    public record RoomMemberDto(
        Guid Id,
        string Name,
        string ProfileImagePath
    );
}
