using MediatR;

namespace Game.Application.Rooms.Commands
{
    public sealed record JoinRoom(Guid RoomId, string RoomCode) : IRequest;
}
