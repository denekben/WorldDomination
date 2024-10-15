using User.Domain.Exceptions;

namespace User.Domain.ValueObjects
{
    public record Bio
    {
        public string Value { get; private set; } = string.Empty;

        private Bio(string? value)
        {
            Value = value ?? string.Empty;
        }

        public static Bio Create(string? value = null)
        {
            if (value?.Length > 150) {
                throw new InvalidArgumentDomainException("Bio length must me 150 or less");
            }

            return new Bio(value);
        }

        public static implicit operator Bio(string? value) => Create(value);
        public static implicit operator string(Bio bio) => bio.Value;
    }
}
