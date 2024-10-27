using Game.Domain.CountryAggregate.Entities;
using Game.Domain.RoomAggregate.Abstractions;
using Game.Domain.RoomAggregate.ValueObjects;
using WorldDomination.Shared.Domain;

namespace Game.Domain.RoomAggregate.Entities
{
    public sealed class Player : RoomMember
    {
        public GameRole GameRole { get; private set; }

        public IdValueObject CountryId { get; private set; }
        public Country Country { get; private set; }

        //EF
        private Player() { }

        private Player(Guid gameRoomId, string name, string path, GameRole gameRole, Guid countryId) 
            : base(gameRoomId, name, path) 
        {
            GameRole = gameRole;
            CountryId = countryId;
        }

        public static Player Create(Guid gameRoomId, string name, string path, GameRole gameRole, Guid countryId)
        {
            return new Player(gameRoomId, name, path, gameRole, countryId);
        }
    }
}
