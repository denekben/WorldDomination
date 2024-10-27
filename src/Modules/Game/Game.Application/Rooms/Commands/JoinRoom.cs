using MediatR;

namespace Game.Application.Rooms.Commands
{
    public sealed record JoinRoom(Guid roomId) : IRequest<Guid>;
}
