using MediatR;

namespace Game.Application.Games.Commands
{
    public sealed record CreateGame(string GameType, bool HasTeams, int RoundQuantity, Guid RoomId) : IRequest<Guid>;
}
