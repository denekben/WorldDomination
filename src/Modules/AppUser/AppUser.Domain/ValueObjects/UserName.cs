using AppUser.Domain.Exceptions;
using System.Runtime.CompilerServices;

namespace UserAccess.Domain.ValueObjects
{
    public record UserName
    {
        public string Value { get; }
        public UserName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new EmptyUserNameException();
            }

            if (value.Length < 1 || value.Length > 15)
            {
                throw new InvalidUserNameLengthException(value.Length);
            }

            Value = value;
        }

        public static implicit operator UserName(string value) => new UserName(value);

        public static implicit operator string(UserName value) => value.Value;
    }
}
