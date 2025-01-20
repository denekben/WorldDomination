using Game.Domain.DomainModels.Games.Entities;
using Game.Domain.DomainModels.ReadModels.Games;
using WorldDomination.Shared.Domain;

namespace Game.Domain.ReadModels.Messaging
{
    public sealed class NegotiationRequestReadModel
    {
        public Guid IssuerCountryId { get; private set; }
        public Guid AudienceCountryId { get; private set; }
        public Guid IssuerMemberId { get; private set; }
        public bool IsApplied { get; private set; }
        public CountryReadModel Issuer { get; private set; }
        public CountryReadModel Audience { get; private set; }
    }
}
