using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Game.Application.Rooms.Commands.Handlers
{
    internal sealed class LeaveRoomHandler : IRequestHandler<LeaveRoom>
    {
        public Task Handle(LeaveRoom request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
