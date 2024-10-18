using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace User.Domain.ValueObjects
{
    public sealed record Name
    {
        public string? Value { get; private set; }

        private Name(string? value)
        {
            Value = value;
        }

        public static Name Create(string? value = null)
        {
            if(value?.Length > 50)
            {
                throw new InvalidArgumentDomainException("Name length must be 50 or less");
            }
            return new Name(value);
        }

        public static implicit operator Name(string? value) => Create(value);
        public static implicit operator string?(Name value) => value?.Value;
    }
}
