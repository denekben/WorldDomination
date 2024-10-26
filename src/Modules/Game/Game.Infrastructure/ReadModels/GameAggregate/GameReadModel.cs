using Game.Infrastructure.ReadModels.CountryAggregate;
using Game.Infrastructure.ReadModels.RoomAggregate;

namespace Game.Infrastructure.ReadModels.GameAggregate
{
    public sealed class GameReadModel
    {
        public Guid Id { get; private set; }
        public string GameType { get; private set; }
        public int CurrentRound { get; private set; }
        public int EcologyLevel { get; private set; }

        public List<CountryReadModel> Countries { get; private set; }
        public Guid RoomId { get; private set; }
        public RoomReadModel Room { get; private set; }
    }
}
