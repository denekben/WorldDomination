using Game.Application.DTOs;
using MediatR;

namespace Game.Application.UseCases.Rooms.Commands
{
    public sealed record JoinRoom(Guid CallerId, Guid RoomId, string? RoomCode) : IRequest<RoomDto>;
}
