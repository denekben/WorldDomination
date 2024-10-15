using User.Domain.Exceptions;

namespace User.Domain.ValueObjects
{
    public record Name
    {
        public string Value { get; private set; } = string.Empty;

        private Name(string? value)
        {
            Value = value ?? string.Empty;
        }

        public static Name Create(string? value)
        {
            if(value?.Length > 50)
            {
                throw new InvalidArgumentDomainException("Name length must be 50 or less");
            }
            return new Name(value);
        }

        public static implicit operator Name(string? value) => Create(value);
        public static implicit operator string(Name value) => value.Value;
    }
}
