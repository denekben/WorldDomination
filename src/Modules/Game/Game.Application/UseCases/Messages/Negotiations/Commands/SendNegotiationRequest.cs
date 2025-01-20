using MediatR;

namespace Game.Application.UseCases.Messages.Negotiations.Commands
{
    public sealed record SendNegotiationRequest(
        Guid CallerId, Guid RoomId, Guid AudienceCountryId
        ) : IRequest;
}
