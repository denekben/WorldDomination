using MediatR;

namespace Game.Application.UseCases.Messages.Teams.Commands
{
    public sealed record SendTeamMessage(Guid CallerId, Guid RoomId, string MessageText) : IRequest;
}
