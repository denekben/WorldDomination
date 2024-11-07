using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.Games.ValueObjects
{
    public sealed record DevelopmentLevel
    {
        private const int _defaultLevel = 60;
        private const int _minLevel = 0;

        public int Value { get; private set; }

        private DevelopmentLevel(int value)
        {
            Value = value;
        }

        public static DevelopmentLevel Create(int value = _defaultLevel)
        {
            if (value < _minLevel)
                throw new InvalidArgumentDomainException($"Development level {value} is invalid");
            
            return new DevelopmentLevel(value);
        }

        public static implicit operator DevelopmentLevel(int value) => Create(value);
        public static implicit operator int(DevelopmentLevel level) => level.Value;
    }
}

