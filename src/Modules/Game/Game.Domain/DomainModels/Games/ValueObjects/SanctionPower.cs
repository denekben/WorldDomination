using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.Games.ValueObjects
{
    public sealed record SanctionPower
    {
        private const int _minValue = 0;
        private const int _defaultValue = 50;

        public int Value { get; private set; }

        private SanctionPower(int value)
        {
            Value = value;
        }

        public static SanctionPower Create(int value = _defaultValue)
        {
            if (value < _minValue)
                throw new InvalidArgumentDomainException($"SanctionPower value {value} is invalid");
            
            return new SanctionPower(value);
        }

        public static implicit operator int(SanctionPower value) => Create(value);
        public static implicit operator SanctionPower(int value) => new SanctionPower(value);
    }
}
