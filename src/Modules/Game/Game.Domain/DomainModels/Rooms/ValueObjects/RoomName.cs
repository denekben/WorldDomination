using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.Rooms.ValueObjects
{
    public sealed record RoomName
    {
        private const int _maxLength = 50;
        private const int _minLength = 1;

        public string Value { get; private set; }

        private RoomName(string value)
        {
            Value = value;
        }

        public static RoomName Create(string value)
        {
            if(value.Length > _maxLength || value.Length < _minLength)
                throw new InvalidArgumentDomainException($"RoomName value {value} is invalid");
            
            return new RoomName(value);
        }

        public static implicit operator string(RoomName value) => value.Value;
        public static implicit operator RoomName(string value) => Create(value);
    }
}
