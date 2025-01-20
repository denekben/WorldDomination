using Game.Domain.DomainModels.Games.Entities;
using WorldDomination.Shared.Domain;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.Messaging.Entities
{
    public sealed class NegotiationRequest : DomainEntity
    {
        public IdValueObject IssuerCountryId { get; private set; }
        public IdValueObject AudienceCountryId { get; private set; }
        public IdValueObject IssuerMemberId { get; private set; }
        public bool IsApplied { get; private set; }
        public Country Issuer { get; private set; }
        public Country Audience { get; private set; }

        //EF
        private NegotiationRequest() { }

        private NegotiationRequest(Guid issuerCountryId, Guid audienceCountryId, Guid issuerMemberId)
        {
            IssuerCountryId = issuerCountryId;
            AudienceCountryId = audienceCountryId;
            IssuerMemberId = issuerMemberId;
            IsApplied = false;
        }

        public static NegotiationRequest Create(Guid issuerCountryId, Guid audienceCountryId, Guid issuerMemberId)
        {
            if (issuerCountryId == audienceCountryId)
                throw new BusinessRuleValidationException("Sanction Issuer cannot be Request Audience");

            return new NegotiationRequest(issuerCountryId, audienceCountryId, issuerMemberId);
        }

        public void Apply()
        {
            IsApplied = true;
        }
    }
}
