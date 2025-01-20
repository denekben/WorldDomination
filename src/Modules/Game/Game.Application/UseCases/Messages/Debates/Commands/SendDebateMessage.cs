using MediatR;

namespace Game.Application.UseCases.Messages.Debates.Commands
{
    public sealed record SendDebateMessage(Guid CallerId, Guid RoomId, string MessageText) : IRequest;
}
