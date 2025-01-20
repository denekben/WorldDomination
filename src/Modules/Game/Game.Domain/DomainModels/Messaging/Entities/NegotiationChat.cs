using Game.Domain.DomainModels.Games.Entities;
using WorldDomination.Shared.Domain;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.Messaging.Entities
{
    public sealed class NegotiationChat : DomainEntity
    {
        public IdValueObject Id { get; private set; }
        public IdValueObject FirstCountryId { get; private set; }
        public IdValueObject SecondCountryId { get; private set; }
        public Country FirstCountry { get; private set; }
        public Country SecondCountry { get; private set; }
        public List<Message> Messages { get; private set; } = [];

        // EF
        private NegotiationChat() { }

        private NegotiationChat( Guid firstCountryId, Guid secondCountryId)
        {
            Id = Guid.NewGuid();
            FirstCountryId = firstCountryId;
            SecondCountryId = secondCountryId;
        }

        public static NegotiationChat Create(Guid firstCountryId, Guid secondCountryId)
        {
            if (secondCountryId == firstCountryId)
                throw new BusinessRuleValidationException("Cannot duplicate countries in chat");

            return new(firstCountryId, secondCountryId);
        }

        public void AddMessage(Message message)
        {
            Messages.Add(message);
        }
    }
}
