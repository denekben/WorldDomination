using WorldDomination.Shared.Domain;
using Game.Domain.DomainModels.UserAggregate.Entities;
using Game.Domain.DomainModels.RoomAggregate.ValueObjects;
using Game.Domain.DomainModels.GameAggregate.Entities;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.RoomAggregate.Entities
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

        protected RoomMember(Guid gameUserId, Guid roomId, string name, string path, GameRole gameRole, Guid countryId)
        {
            GameUserId = gameUserId;
            RoomId = roomId;
            Name = name;
            ProfileImagePath = path;
            GameRole = gameRole;
            CountryId = countryId;
        }

        public void PromoteToRole(GameRole gameRole)
        {
            if(gameRole == GameRole.Minister)
                GameRole = GameRole.Minister;
            else if(gameRole == GameRole.President)
                GameRole = GameRole.President;
            else
                throw new BusinessRuleValidationException($"Can promote Member to only Minister and President roles");
                
        }
    }
}
