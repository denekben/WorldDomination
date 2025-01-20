using Game.Domain.DomainModels.Messaging.ValueObjects;
using Game.Domain.DomainModels.Rooms.Entities;
using WorldDomination.Shared.Domain;

namespace Game.Domain.DomainModels.Messaging.Entities
{
    public sealed class Message : DomainEntity
    {
        public IdValueObject Id {  get; private set; }
        public MessageText MessageText { get; private set; }
        public IdValueObject IssuerId { get; private set; }
        public RoomMember Issuer { get; private set; }
        public IdValueObject ChatId { get; private set; }

        // EF
        private Message() { }

        private Message(Guid issuerId, Guid chatId, string messageText)
        {
            Id = Guid.NewGuid();
            MessageText = messageText;
            IssuerId = issuerId;
            ChatId = chatId;
        }

        public static Message Create(Guid issuerId, Guid chatId, string messageText)
        {
            return new(issuerId, chatId, messageText);
        }
    }
}
