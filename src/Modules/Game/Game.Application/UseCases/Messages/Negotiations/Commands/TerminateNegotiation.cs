using MediatR;

namespace Game.Application.UseCases.Messages.Negotiations.Commands
{
    public sealed record TerminateNegotiation(Guid CallerId, Guid RoomId, Guid ChatId) : IRequest;
}
