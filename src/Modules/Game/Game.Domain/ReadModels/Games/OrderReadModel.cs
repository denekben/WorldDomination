using Game.Domain.DomainModels.ReadModels.Games;
using WorldDomination.Shared.Domain;

namespace Game.Domain.ReadModels.Games
{
    public sealed class OrderReadModel
    {
        public Guid CountryId { get; private set; }
        public List<Guid> CitiesToDevelop { get; private set; }
        public List<Guid> CitiesToSetShield { get; private set; }
        public bool DevelopEcologyProgram { get; private set; }
        public bool DevelopNuclearTechology { get; private set; }
        public int BombsToBuyQuantity { get; private set; }
        public List<Guid> CitiesToStrike { get; private set; }
        public List<Guid> CountriesToSetSanctions { get; private set; }
        public Dictionary<Guid, int> CountriesToDonate { get; private set; }

        public CountryReadModel Country { get; private set; }
        public Guid RoomId { get; private set; }
    }
}
