using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.GameAggregate.ValueObjects
{
    public sealed record LivingLevel
    {
        private const int _minLivingLevel = 0;
        private const int _maxLivingLevel = 100;
        private const int _defaultLivingLevel = 0;

        public int Value { get; private set; }

        private LivingLevel(int value)
        {
            Value = value;
        }

        public static LivingLevel Create(int? value = null)
        {
            if (value < _minLivingLevel || value > _maxLivingLevel)
            {
                throw new InvalidArgumentDomainException($"LivingLevel value {value} is invalid");
            }
            return new LivingLevel(value ?? _defaultLivingLevel);
        }

        public static implicit operator LivingLevel(int value) => Create(value);
        public static implicit operator int(LivingLevel level) => level.Value;
    }
}
