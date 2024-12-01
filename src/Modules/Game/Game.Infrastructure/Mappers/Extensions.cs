using Game.Domain.DomainModels.Rooms.Entities;
using Game.Domain.DomainModels.ReadModels.Rooms;
using Game.Application.DTOs;

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
                room.IsPrivate,
                room.CreatedTime,
                members
            );
        }

        public static RoomMemberDto AsRoomMemberDto(this RoomMemberReadModel member)
        {
            return new RoomMemberDto(
                member.GameUserId,
                member.Name,
                member.ProfileImagePath,
                member.RoomId
            );
        }

        public static RoomDto AsRoomDto(this Room room)
        {
            var members = room.RoomMembers.Select(m => m.AsRoomMemberDto()).ToList();
            return new RoomDto(
                room.Id,
                room.RoomName,
                room.GameType,
                room.IsPrivate,
                room.CreatedTime ?? DateTime.UtcNow,
                members
            );
        }

        public static RoomMemberDto AsRoomMemberDto(this RoomMember member)
        {
            return new RoomMemberDto(
                member.GameUserId,
                member.Name,
                member.ProfileImagePath,
                member.RoomId
            );
        }
    }
}