using MediatR;

namespace Game.Application.UseCases.Games.Commands
{
    public sealed record ChangeGameState(Guid CallerId, Guid RoomId, Guid GameId) : IRequest;
}
