using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.Games.ValueObjects
{
    public sealed record Budget
    {
        private const int _defaultBudget = 1000;
        private const int _minBudget = 0;

        public int Value { get; private set; }

        private Budget(int value)
        {
            Value = value;
        }

        public static Budget Create(int value = _defaultBudget)
        {
            if (value < _minBudget)
                throw new InvalidArgumentDomainException($"Budget value {value} is invalid");
            
            return new Budget(value);
        }

        public static implicit operator Budget(int value) => Create(value);
        public static implicit operator int(Budget value) => value.Value;
    }
}
