using Game.Domain.DomainModels.ReadModels.Games;

namespace Game.Domain.ReadModels.Games
{
    public class SanctionReadModel
    {
        public Guid IssuserId { get; private set; }
        public Guid AudienceId { get; private set; }
        public float SanctionPower { get; private set; }
        public CountryReadModel Issuser { get; private set; }
        public CountryReadModel Audience { get; private set; }
    }
}
