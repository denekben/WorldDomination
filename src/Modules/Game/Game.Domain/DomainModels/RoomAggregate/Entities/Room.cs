using Game.Domain.RoomAggregate.ValueObjects;
using Game.Domain.UserAggregate.Entities;
using DomainGame = Game.Domain.GameAggregate.Entities.Game;
using WorldDomination.Shared.Domain;
using WorldDomination.Shared.Exceptions.CustomExceptions;
using Game.Domain.DomainModels.RoomAggregate.Abstractions;

namespace Game.Domain.RoomAggregate.Entities
{
    public sealed class Room : DomainEntity
    {
        public IdValueObject Id { get; private set; }
        public RoomName RoomName { get; private set; }
        public GameType GameType { get; private set; }
        public RoomMemberLimit RoomMemberLimit { get; private set; }
        public CountryQuantity CountryQuantity { get; private set; }
        public bool IsPrivate { get; private set; }
        public string? RoomCode { get; private set; }
        public DateTime? CreatedTime { get; private set; }

        public DomainGame DomainGame { get; private set; }
        public IdValueObject CreatorId { get; private set; }
        public GameUser Creator { get; private set; }
        public List<RoomMember> RoomMembers { get; private set; } = [];


        // EF
        private Room() { }

        private Room(Guid creatorId, string roomName, string gameType, 
            int roomLimit, int countryQuantity, bool isPrivate, string? roomCode)
        {
            Id = Guid.NewGuid();
            CreatorId = creatorId;
            RoomName = roomName;
            GameType = gameType;
            RoomMemberLimit = roomLimit;
            CountryQuantity = countryQuantity;
            IsPrivate = isPrivate;
            RoomCode = roomCode;
        }

        public static Room Create(Guid creatorId, string roomName, string gameType,
            int roomLimit, int countryQuantity, bool isPrivate, string? roomCode)
        {
            return new Room(creatorId, roomName, gameType, 
                roomLimit, countryQuantity, isPrivate, roomCode);
        }

        public void AddMember(RoomMember member, string? roomCode = null)
        {
            if(RoomMembers.Any(m => m.GameUserId == member.GameUserId))
                throw new BusinessRuleValidationException("Cannot add same user in room");

            if ((RoomMembers.Count() == 0 && member is not Organizer))
                throw new BusinessRuleValidationException("First user in room must be organizer");

            if (RoomMembers.Count >= RoomMemberLimit && member is not Organizer)
                throw new BusinessRuleValidationException("Member quantity cannot exceed the limit");

            if(RoomMembers.Where(m => (m is Organizer)).Count() >= 1 && (member is Organizer))
                throw new BusinessRuleValidationException("Room can have only one organizer");

            if (IsPrivate && roomCode!=RoomCode)
                throw new BusinessRuleValidationException("Invalid RoomCode");

            RoomMembers.Add(member);
        }

        public void RemoveMember(RoomMember member)
        {
            if (RoomMembers.Count(m => m.GameUserId == member.GameUserId) == 0)
                throw new BusinessRuleValidationException("To remove a user from a Room, they must belong to that Room");

            if(RoomMembers.Count() < 1)
                throw new BusinessRuleValidationException("Room must have at least 1 RoomMember");

            RoomMembers.Remove(member);
        }

        public Organizer ElectNewOrganizer(string? roomCode = null)
        {
            if (RoomMembers.FirstOrDefault(m=>(m is Organizer)) != null)
                throw new BusinessRuleValidationException("Cannot elect a new Organizer in a Room with the Organizer");

            var organizerCandidate = RoomMembers.FirstOrDefault(m=>(m is Player))
                ?? throw new BusinessRuleValidationException("Room must have at least 1 Player to promote him to Organizer");

            RemoveMember(organizerCandidate);
            var newOrganizer = Organizer.PromoteToOrganizer(organizerCandidate);
            AddMember(newOrganizer, roomCode);

            return newOrganizer;
        }
    }
}