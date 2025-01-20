using Game.Application.DTOs;
using MediatR;

namespace Game.Application.UseCases.Rooms.Commands
{
    public sealed record CreateRoom(Guid CallerId, string? RoomName, string GameType, bool HasTeams,
        int MemberLimit, int RoundQuantity, int CountryLimit, bool IsPrivate, string? RoomCode) : IRequest<RoomDto>;
}
