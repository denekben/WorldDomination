using Game.Domain.RoomAggregate.Abstractions;
using Game.Domain.RoomAggregate.ValueObjects;
using Game.Domain.UserAggregate.Entities;
using DomainGame = Game.Domain.GameAggregate.Entities.Game;
using WorldDomination.Shared.Domain;

namespace Game.Domain.RoomAggregate.Entities
{
    public sealed class Room
    {
        public IdValueObject Id { get; private set; }
        public RoomName RoomName { get; private set; }
        public GameType GameType { get; private set; }
        public RoomMemberLimit RoomMemberLimit { get; private set; }
        public CountryQuantity CountryQuantity { get; private set; }
        public bool IsPublic { get; private set; }
        public RoomCode RoomCode { get; private set; }
        public DateTime? CreatedTime { get; private set; }
        public DomainGame DomainGame { get; private set; }
        public IdValueObject CreatorId { get; private set; }
        public GameUser Creator { get; private set; }
        public List<RoomMember> RoomMembers { get; private set; } = [];


        // EF
        private Room() { }

        private Room(Guid creatorId, string roomName, string gameType, 
            int roomLimit, int countryQuantity, bool isPublic, string? roomCode)
        {
            Id = new Guid();
            CreatorId = creatorId;
            RoomName = roomName;
            GameType = gameType;
            RoomMemberLimit = roomLimit;
            CountryQuantity = countryQuantity;
            IsPublic = isPublic;
            RoomCode = RoomCode.Create(roomCode);
        }

        public static Room Create(Guid creatorId, string roomName, string gameType,
            int roomLimit, int countryQuantity, bool isPublic, string? roomCode)
        {
            return new Room(creatorId, roomName, gameType, 
                roomLimit, countryQuantity, isPublic, roomCode);
        }
    }
}
