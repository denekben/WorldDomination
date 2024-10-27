using Game.Shared.DTOs;
using MediatR;

namespace Game.Application.Rooms.Queries
{
    public sealed record SearchRooms(string? SearchPhrase, int PageNumber = 1, int PageSize = 10) : IRequest<List<RoomDto>>;
}
