using MediatR;

namespace Game.Application.UseCases.Messages.Negotiations.Commands
{
    public sealed record ApplyNegotiationRequest(Guid CallerId, Guid IssuerCountryId, Guid RoomId) : IRequest;
}
