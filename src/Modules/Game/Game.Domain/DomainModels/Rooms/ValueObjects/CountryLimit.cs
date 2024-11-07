using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.Rooms.ValueObjects
{
    public sealed record CountryLimit
    {
        private const int _maxQuantity = 11;
        private const int _minQuantity = 2;
        private const int _defaultQuantity = 11;

        public int Value { get; private set; }

        private CountryLimit(int value)
        {
            Value = value;
        }

        public static CountryLimit Create(int value = _defaultQuantity)
        {
            if (value < _minQuantity || value > _maxQuantity)
                throw new InvalidArgumentDomainException($"CountryQuantity value {value} is invalid");

            return new CountryLimit(value);
        }

        public static implicit operator CountryLimit(int value) => Create(value);
        public static implicit operator int(CountryLimit value) => value.Value;
    }
}
