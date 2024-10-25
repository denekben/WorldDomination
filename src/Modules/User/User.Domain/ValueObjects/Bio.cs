using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace User.Domain.ValueObjects
{
    public sealed record Bio
    {
        private const int _maxLength = 150;

        public string Value { get; private set; }

        private Bio(string value)
        {
            Value = value;
        }

        public static Bio Create(string? value = null)
        {
            if (value?.Length > _maxLength) {
                throw new InvalidArgumentDomainException("Bio length must me 150 or less");
            }
            return new Bio(value ?? string.Empty);
        }

        public static implicit operator Bio(string value) => Create(value);
        public static implicit operator string(Bio bio) => bio.Value;
    }
}
