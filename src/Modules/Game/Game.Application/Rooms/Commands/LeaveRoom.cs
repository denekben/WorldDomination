using MediatR;

namespace Game.Application.Rooms.Commands
{
    public sealed record LeaveRoom(Guid roomId) : IRequest;
}
