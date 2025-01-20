using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.Messaging.ValueObjects
{
    public sealed record MessageText
    {
        private const int _minMessageLength = 1;
        private const int _maxMessageLength = 200;

        public string Value { get; private set; }

        private MessageText(string value)
        {
            Value = value;    
        }

        public static MessageText Create(string value) { 
            if(string.IsNullOrWhiteSpace(value) || value.Length < _minMessageLength || value.Length > _maxMessageLength)
            {
                throw new InvalidArgumentDomainException($"Value {value} for MessageText is invalid");
            }
            return new MessageText(value);
        }

        public static implicit operator MessageText(string value) => new MessageText(value);
        public static implicit operator string(MessageText messageText) => messageText.Value;
    }
}
