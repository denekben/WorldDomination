using MediatR;

namespace Game.Application.Games.Commands
{
    public sealed record ChangeGameState(Guid MemberId, Guid RoomId, Guid GameId) : IRequest;
}
