using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.RoomAggregate.ValueObjects
{
    public sealed record CountryQuantity
    {
        private const int _maxQuantity = 10;
        private const int _minQuantity = 2;

        public int Value { get; private set; }

        private CountryQuantity(int value)
        {
            Value = value;
        }

        public static CountryQuantity Create(int value)
        {
            if (value < _minQuantity || value > _maxQuantity)
            {
                throw new InvalidArgumentDomainException($"CountryQuantity value {value} is invalid");
            }

            return new CountryQuantity(value);
        }

        public static implicit operator CountryQuantity(int value) => Create(value);
        public static implicit operator int(CountryQuantity value) => value.Value;
    }
}
