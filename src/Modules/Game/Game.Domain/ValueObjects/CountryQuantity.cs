using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.ValueObjects
{
    public sealed record CountryQuantity
    {
        public int Value { get; private set; }

        private CountryQuantity(int value) {
            Value = value;
        }

        public static CountryQuantity Create(int value)
        {
            if(value < 2 || value > 10) { 
                throw new InvalidArgumentDomainException($"CountryQuantity value {value} is invalid");
            }

            return new CountryQuantity(value);
        }

        public static implicit operator CountryQuantity(int value) => Create(value);
        public static implicit operator int(CountryQuantity value) => value.Value;
    }
}
