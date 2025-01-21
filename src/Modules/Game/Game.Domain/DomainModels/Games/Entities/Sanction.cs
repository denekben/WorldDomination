using Game.Domain.DomainModels.Games.ValueObjects;
using WorldDomination.Shared.Domain;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.Games.Entities
{
    public sealed class Sanction : DomainEntity
    {
        public IdValueObject IssuerId { get; private set; }
        public IdValueObject AudienceId { get; private set; }
        public SanctionPower SanctionPower { get; private set; }
        public Country Issuer {  get; private set; }
        public Country Audience { get; private set; }

        //EF
        private Sanction() { }

        private Sanction(Guid issuerId, Guid audienceId, 
            int sanctionPower)
        {
            IssuerId = issuerId;
            AudienceId = audienceId;
            SanctionPower = SanctionPower.Create(sanctionPower);
        }

        public static Sanction Create(Guid issuerId, Guid audienceId, int sanctionPower)
        {
            if (issuerId == audienceId)
                throw new BusinessRuleValidationException("Sanction Issuer cannot be Sanction Audience");

            return new Sanction(issuerId, audienceId, sanctionPower);
        }
    }
}
