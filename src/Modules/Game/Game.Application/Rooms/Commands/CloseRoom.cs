using MediatR;

namespace Game.Application.Rooms.Commands
{
    public sealed record CloseRoom(Guid RoomId) : IRequest;
}
