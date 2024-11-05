using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace Game.Domain.DomainModels.Games.ValueObjects
{
    public sealed record Income
    {
        private const int _minIncome = 0;
        private const int _defaultIncome = 0;

        public int Value { get; private set; }

        private Income(int value)
        {
            Value = value;
        }

        public static Income Create(int? value = null)
        {
            if (value < _minIncome)
            {
                throw new InvalidArgumentDomainException($"Income value {value} is invalid");
            }
            return new Income(value ?? _defaultIncome);
        }

        public static implicit operator Income(int value) => Create(value);
        public static implicit operator int(Income income) => income.Value;
    }
}

