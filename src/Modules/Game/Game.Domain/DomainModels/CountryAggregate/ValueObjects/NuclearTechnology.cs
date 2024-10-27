using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.CountryAggregate.ValueObjects
{
    public sealed record NuclearTechnology
    {
        private const int _minNBQuantity = 0;

        public int Value { get; private set; }

        private NuclearTechnology(bool haveNuclearTechnology, int value)
        {
            Value = value;
        }

        public static NuclearTechnology Create(int? nuclearBombQuantity = null)
        {
            if (nuclearBombQuantity < _minNBQuantity)
            {
                throw new InvalidArgumentDomainException($"NuclearTechnology value {nuclearBombQuantity} is invalid");
            }
            return new NuclearTechnology(nuclearBombQuantity ?? _minNBQuantity);
        }

        public static implicit operator NuclearTechnology(int value) => Create(value);
        public static implicit operator int(NuclearTechnology value) => value.Value;
    }
}
