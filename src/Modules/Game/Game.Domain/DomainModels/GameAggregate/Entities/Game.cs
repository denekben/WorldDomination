using Game.Domain.DomainModels.GameAggregate.ValueObjects;
using Game.Domain.DomainModels.RoomAggregate.Entities;
using Game.Domain.DomainModels.RoomAggregate.ValueObjects;
using WorldDomination.Shared.Domain;

namespace Game.Domain.DomainModels.GameAggregate.Entities
{
    public sealed class Game : DomainEntity
    {
        public IdValueObject RoomId { get; private set; }
        public GameType GameType { get; private set; }
        public bool HasTeams { get; private set; }
        public CurrentRound CurrentRound { get; private set; }
        public EcologyLevel EcologyLevel { get; private set; }

        public List<Country> Countries { get; private set; }
        public Room Room { get; private set; }

        //EF
        private Game() { }

        private Game(GameType gameType, bool hasTeams, Guid roomId)
        {
            RoomId = roomId;
            GameType = gameType;
            HasTeams = hasTeams;
            CurrentRound = CurrentRound.Create();
            EcologyLevel = EcologyLevel.Create();
        }

        public static Game Create(GameType gameType, bool hasTeams, Guid roomId)
        {
            return new Game(gameType, hasTeams, roomId);
        }
    }
}
