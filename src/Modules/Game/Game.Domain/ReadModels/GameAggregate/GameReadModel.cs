using Game.Domain.DomainModels.ReadModels.RoomAggregate;

namespace Game.Domain.DomainModels.ReadModels.GameAggregate
{
    public sealed class GameReadModel
    {
        public Guid RoomId { get; private set; }
        public string GameType { get; private set; }
        public bool HasTeams { get; private set; }
        public int CurrentRound { get; private set; }
        public int EcologyLevel { get; private set; }

        public List<CountryReadModel> Countries { get; private set; }
        public RoomReadModel Room { get; private set; }
    }
}
