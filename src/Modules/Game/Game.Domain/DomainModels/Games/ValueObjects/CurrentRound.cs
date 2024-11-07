using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.Games.ValueObjects
{
    public sealed record CurrentRound
    {
        private const int _minRound = 1;
        private const int _defaultRound = 1;

        public int Value { get; private set; }

        private CurrentRound(int value)
        {
            Value = value;
        }

        public static CurrentRound Create(int value = _defaultRound)
        {
            if (value < _minRound)
                throw new InvalidArgumentDomainException($"CurrentRound value {value} is invalid");
            
            return new CurrentRound(value);
        }

        public static implicit operator CurrentRound(int value) => Create(value);
        public static implicit operator int(CurrentRound value) => value.Value;
    }
}
