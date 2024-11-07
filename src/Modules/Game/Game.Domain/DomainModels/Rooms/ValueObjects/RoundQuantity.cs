using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.Rooms.ValueObjects
{
    public sealed record RoundQuantity
    {
        private const int _minRoundQuantity = 2;
        private const int _maxRoundQuantity = 10;

        public int Value { get; private set; }

        private RoundQuantity(int value)
        {
            Value = value;
        }

        public static RoundQuantity Create(int value)
        {
            if(value < _minRoundQuantity || value > _maxRoundQuantity)
            {
                throw new InvalidArgumentDomainException($"Invalid value {value} for RoundQuantity");
            }

            return new RoundQuantity(value);
        }

        public static implicit operator int(RoundQuantity value) => value.Value;
        public static implicit operator RoundQuantity(int value) => new(value);
    }
}
