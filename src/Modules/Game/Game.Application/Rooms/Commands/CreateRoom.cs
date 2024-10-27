using MediatR;

namespace Game.Application.Rooms.Commands
{
    public sealed record CreateRoom(string roomName, string gameType,
        int roomLimit, int countryQuantity, bool isPublic, string? roomCode) : IRequest<Guid>;
}
