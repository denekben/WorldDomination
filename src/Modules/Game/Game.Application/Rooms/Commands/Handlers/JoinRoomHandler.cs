using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Game.Application.Rooms.Commands.Handlers
{
    internal sealed class JoinRoomHandler : IRequestHandler<JoinRoom, Guid>
    {
        public Task<Guid> Handle(JoinRoom request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
