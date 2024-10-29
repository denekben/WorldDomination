using MediatR;

namespace Game.Application.Rooms.Commands
{
    public sealed record CreateRoom(string? RoomName, string GameType,
        int RoomLimit, int CountryQuantity, bool IsPrivate, string? RoomCode) : IRequest<Guid>;
}
