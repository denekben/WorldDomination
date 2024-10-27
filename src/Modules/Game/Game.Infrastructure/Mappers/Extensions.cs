using Game.Domain.RoomAggregate.Abstractions;
using Game.Domain.RoomAggregate.Entities;
using Game.Domain.ReadModels.RoomAggregate;
using Game.Shared.DTOs;

namespace Game.Infrastructure.Mappers
{
    public static class Extensions
    {
        public static RoomDto AsRoomDto(this RoomReadModel room)
        {
            var members = room.RoomMembers.Select(m => m.AsRoomMemberDto()).ToList();
            return new RoomDto(
                room.Id,
                room.RoomName,
                room.GameType,
                room.CountryQuantity,
                room.IsPublic,
                room.CreatedTime,
                members
            );
        }

        public static RoomMemberDto AsRoomMemberDto(this RoomMemberReadModel member)
        {
            return new RoomMemberDto(
                member.Id,
                member.Name,
                member.ProfileImagePath
            );
        }

        public static RoomDto AsRoomDto(this Room room)
        {
            var members = room.RoomMembers.Select(m => m.AsRoomMemberDto()).ToList();
            return new RoomDto(
                room.Id,
                room.RoomName,
                room.GameType,
                room.CountryQuantity,
                room.IsPublic,
                room.CreatedTime ?? DateTime.UtcNow,
                members
            );
        }

        public static RoomMemberDto AsRoomMemberDto(this RoomMember member)
        {
            return new RoomMemberDto(
                member.Id,
                member.Name,
                member.ProfileImagePath
            );
        }
    }
}