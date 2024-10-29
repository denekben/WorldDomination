using Game.Domain.ReadModels.GameAggregate;
using Game.Domain.ReadModels.RoomAggregate;

namespace Game.Domain.ReadModels.CountryAggregate
{
    public sealed class CountryReadModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string FlagImagePath { get; private set; }
        public int LivingLevel { get; private set; }
        public int Budget { get; private set; }
        public bool HaveNuclearTechnology { get; private set; }
        public int NuclearTecnology { get; private set; }
        public int SanctionCount { get; private set; }
        public List<RoomMemberReadModel> Players { get; private set; }
        public List<CityReadModel> Cities { get; private set; }
        public Guid GameId { get; private set; }
        public GameReadModel Game { get; private set; }
    }
}
