using MediatR;

namespace Game.Application.UseCases.Games.Commands
{
    public sealed record CreateGame(Guid CallerId, string GameType, bool HasTeams, bool HasGameStateTimer, int RoundQuantity, Guid RoomId) : IRequest<Guid>;
}
