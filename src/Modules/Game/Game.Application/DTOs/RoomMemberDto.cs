namespace Game.Application.DTOs
{
    public record RoomMemberDto(
        Guid Id,
        string Name,
        string ProfileImagePath,
        Guid RoomId
    );
}
