using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.Games.ValueObjects
{
    public sealed record NuclearTechnology
    {
        private const int _minNBQuantity = 0;
        private const int _defaultNBQuantity = 0;

        public int Value { get; private set; }

        private NuclearTechnology(int value)
        {
            Value = value;
        }

        public static NuclearTechnology Create(int nuclearBombQuantity = _defaultNBQuantity)
        {
            if (nuclearBombQuantity < _minNBQuantity)
                throw new InvalidArgumentDomainException($"NuclearTechnology value {nuclearBombQuantity} is invalid");
            
            return new NuclearTechnology(nuclearBombQuantity);
        }

        public static implicit operator NuclearTechnology(int value) => Create(value);
        public static implicit operator int(NuclearTechnology value) => value.Value;
    }
}
