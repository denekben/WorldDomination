using Game.Domain.DomainModels.GameAggregate.Entities;
using WorldDomination.Shared.Domain;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.GameAggregate.ValueObjects
{
    public sealed record SanctionCount
    {
        private const int _minSanctionCount = 0;
        private const int _defaultSanctionCount = 0;

        public int Value { get; private set; }

        private SanctionCount(int value)
        {
            Value = value;
        }

        public static SanctionCount Create(int? value = null)
        {
            if (value < _minSanctionCount)
            {
                throw new InvalidArgumentDomainException($"SanctionCount value {value} is invalid");
            }
            return new SanctionCount(value ?? _defaultSanctionCount);
        }

        public static implicit operator SanctionCount(int value) => Create(value);
        public static implicit operator int(SanctionCount value) => value.Value;
    }
}
