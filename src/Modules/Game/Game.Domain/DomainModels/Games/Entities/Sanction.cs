using Game.Domain.DomainModels.Games.ValueObjects;
using WorldDomination.Shared.Domain;

namespace Game.Domain.DomainModels.Games.Entities
{
    public class Sanction : DomainEntity
    {
        public IdValueObject IssuserId { get; private set; }
        public IdValueObject AudienceId { get; private set; }
        public SanctionPower SanctionPower { get; private set; }
        public Country Issuser {  get; private set; }
        public Country Audience { get; private set; }

        //EF
        private Sanction() { }

        private Sanction(Guid issuserId, Guid audienceId, 
            float? sanctionPower = null)
        {
            IssuserId = issuserId;
            AudienceId = audienceId;
            SanctionPower = SanctionPower.Create(sanctionPower);
        }

        public static Sanction Create(Guid issuserId, Guid audienceId,
            float? sanctionPower = null)
        {
            return new Sanction(issuserId, audienceId, sanctionPower);
        }
    }
}
