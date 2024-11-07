using Game.Domain.DomainModels.Games.ValueObjects;
using WorldDomination.Shared.Domain;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.Games.Entities
{
    public sealed class Sanction : DomainEntity
    {
        public IdValueObject IssuserId { get; private set; }
        public IdValueObject AudienceId { get; private set; }
        public SanctionPower SanctionPower { get; private set; }
        public Country Issuser {  get; private set; }
        public Country Audience { get; private set; }

        //EF
        private Sanction() { }

        private Sanction(Guid issuserId, Guid audienceId, 
            float sanctionPower)
        {
            IssuserId = issuserId;
            AudienceId = audienceId;
            SanctionPower = SanctionPower.Create(sanctionPower);
        }

        public static Sanction Create(Guid issuserId, Guid audienceId, float sanctionPower)
        {
            if (issuserId == audienceId)
                throw new BusinessRuleValidationException("Sanction Issuser cannot be Sanction Audience");

            return new Sanction(issuserId, audienceId, sanctionPower);
        }
    }
}
