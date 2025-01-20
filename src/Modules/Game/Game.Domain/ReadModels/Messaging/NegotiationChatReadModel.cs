using Game.Domain.DomainModels.ReadModels.Games;

namespace Game.Domain.ReadModels.Messaging
{
    public sealed class NegotiationChatReadModel
    {
        public Guid Id { get; private set; }
        public Guid FirstCountryId { get; private set; }
        public Guid SecondCountryId { get; private set; }
        public CountryReadModel FirstCountry { get; private set; }
        public CountryReadModel SecondCountry { get; private set; }
        public List<MessageReadModel> Messages { get; private set; } = [];
    }
}
