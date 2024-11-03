using MediatR;

namespace Game.Application.Games.Commands
{
    public sealed record CreateGame(string GameType, bool hasTeams, Guid RoomId) : IRequest<Guid>;
}
