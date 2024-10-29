using WorldDomination.Shared.Domain;
using Game.Domain.RoomAggregate.Entities;
using Game.Domain.UserAggregate.Entities;
using Game.Domain.CountryAggregate.Entities;
using Game.Domain.RoomAggregate.ValueObjects;

namespace Game.Domain.DomainModels.RoomAggregate.Abstractions
{
    public abstract class RoomMember : DomainEntity
    {
        public IdValueObject GameUserId { get; private set; }
        public IdValueObject RoomId { get; private set; }
        public string Name { get; private set; }
        public string ProfileImagePath { get; private set; }
        public GameRole GameRole { get; private set; }

        public IdValueObject? CountryId { get; private set; }
        public Country Country { get; private set; }
        public Room Room { get; private set; }
        public GameUser GameUser { get; private set; }

        protected RoomMember() { }

        protected RoomMember(Guid creatorId, Guid roomId, string name, string path)
        {
            GameUserId = creatorId;
            RoomId = roomId;
            Name = name;
            ProfileImagePath = path;
            GameRole = GameRole.Minister;
        }

        protected RoomMember(Guid gameUserId, Guid roomId, string name, string path, GameRole gameRole, IdValueObject? countryId)
        {
            GameUserId = gameUserId;
            RoomId = roomId;
            Name = name;
            ProfileImagePath = path;
            GameRole = gameRole;
            CountryId = countryId;
        }
    }
}
