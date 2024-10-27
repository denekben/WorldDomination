using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.GameAggregate.ValueObjects
{
    public sealed record CurrentRound
    {
        private const int _minRound = 1;
        private const int _maxRound = 7;
        private const int _defaultRound = 1;

        public int Value { get; private set; }

        private CurrentRound(int value)
        {
            Value = value;
        }

        public static CurrentRound Create(int? value = null)
        {
            if (value < _minRound || value > _maxRound)
            {
                throw new InvalidArgumentDomainException($"CurrentRound value {value} is invalid");
            }
            return new CurrentRound(value ?? _defaultRound);
        }

        public static implicit operator CurrentRound(int value) => Create(value);
        public static implicit operator int(CurrentRound value) => value.Value;
    }
}
