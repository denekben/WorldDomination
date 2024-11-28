using MediatR;

namespace Game.Application.Games.Commands
{
    public sealed record CreateGame(string GameType, bool HasTeams, bool HasGameStateTimer, int RoundQuantity, Guid RoomId) : IRequest<Guid>;
}
