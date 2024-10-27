using MediatR;

namespace Game.Application.Games
{
    public sealed record CreateGame(string GameType, Guid RoomId) : IRequest<Guid>;
}
