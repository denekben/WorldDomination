using MediatR;

namespace Game.Application.UseCases.Rooms.Commands
{
    public sealed record LeaveRoom(Guid CallerId, Guid RoomId) : IRequest;
}
