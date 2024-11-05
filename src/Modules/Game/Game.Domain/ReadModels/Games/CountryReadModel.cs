using Game.Domain.DomainModels.ReadModels.Rooms;
using Game.Domain.ReadModels.Games;

namespace Game.Domain.DomainModels.ReadModels.Games
{
    public sealed class CountryReadModel
    {
        public Guid Id { get; private set; }
        public string CityName { get; private set; }
        public string NormalizedName { get; private set; }
        public string FlagImagePath { get; private set; }
        public int LivingLevel { get; private set; }
        public int Budget { get; private set; }
        public bool HaveNuclearTechnology { get; private set; }
        public int NuclearTecnology { get; private set; }

        public List<RoomMemberReadModel> Players { get; private set; }
        public List<CityReadModel> Cities { get; private set; }
        public List<SanctionReadModel> Sanctions { get; private set; }
        public Guid RoomId { get; private set; }
        public RoomReadModel Room { get; private set; }
        public Guid GameId { get; private set; }
        public GameReadModel Game { get; private set; }
    }
}
