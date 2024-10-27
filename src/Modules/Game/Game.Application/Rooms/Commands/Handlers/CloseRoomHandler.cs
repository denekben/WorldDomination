using MediatR;

namespace Game.Application.Rooms.Commands.Handlers
{
    internal sealed class CloseRoomHandler : IRequestHandler<CloseRoom>
    {
        public Task Handle(CloseRoom request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}