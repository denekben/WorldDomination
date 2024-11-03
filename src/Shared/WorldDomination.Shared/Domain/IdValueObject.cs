using System;

namespace WorldDomination.Shared.Domain
{
    public record IdValueObject
    {
        public Guid Value { get; private set; }

        public IdValueObject(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyIdException();
            }

            Value = value;
        }

        public static implicit operator IdValueObject(Guid id)
            => new(id);

        public static implicit operator Guid(IdValueObject id)
            => id.Value;

        public static bool operator ==(IdValueObject? idVO, Guid id) => idVO?.Value == id;

        public static bool operator !=(IdValueObject? idVO, Guid id) => idVO?.Value != id;
    }
}
