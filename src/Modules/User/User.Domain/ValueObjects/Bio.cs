using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace User.Domain.ValueObjects
{
    public sealed record Bio
    {
        public string? Value { get; private set; }

        private Bio(string? value)
        {
            Value = value;
        }

        public static Bio Create(string? value = null)
        {
            if (value?.Length > 150) {
                throw new InvalidArgumentDomainException("Bio length must me 150 or less");
            }

            return new Bio(value);
        }

        public static implicit operator Bio(string? value) => Create(value);
        public static implicit operator string?(Bio bio) => bio?.Value;
    }
}
