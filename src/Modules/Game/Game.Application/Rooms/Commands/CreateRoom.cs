using MediatR;

namespace Game.Application.Rooms.Commands
{
    public sealed record CreateRoom(string? RoomName, string GameType, bool HasTeams,
        int MemberLimit, int RoundQuantity, int CountryLimit, bool IsPrivate, string? RoomCode) : IRequest<Guid>;
}
