using Game.Application.DTOs;
using MediatR;

namespace Game.Application.UseCases.Rooms.Queries
{
    public sealed record SearchRooms(string? SearchPhrase, int PageNumber = 1, int PageSize = 10) : IRequest<List<RoomDto>>;
}
