using MediatR;

namespace Game.Application.UseCases.Messages.Negotiations.Commands
{
    public sealed record SendNegotiationMessage(Guid CallerId, Guid RoomId, Guid AudienceCountryId, string MessageText) : IRequest;
}
