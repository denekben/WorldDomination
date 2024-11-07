using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.Games.ValueObjects
{
    public sealed record SanctionPower
    {
        private const float _minValue = 0.0f;
        private const float _defaultValue = 1.0f;

        public float Value { get; private set; }

        private SanctionPower(float value)
        {
            Value = value;
        }

        public static SanctionPower Create(float value = _defaultValue)
        {
            if (value < _minValue)
                throw new InvalidArgumentDomainException($"SanctionPower value {value} is invalid");
            
            return new SanctionPower(value);
        }

        public static implicit operator float(SanctionPower value) => Create(value);
        public static implicit operator SanctionPower(float value) => new SanctionPower(value);
    }
}
