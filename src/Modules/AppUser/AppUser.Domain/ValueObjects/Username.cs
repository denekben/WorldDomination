using AppUser.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace AppUser.Domain.ValueObjects
{
    public sealed record Username
    {
        public string Value { get; }
        private const string _regex = @"^[A-Za-z0-9_]{3,20}$";

        public Username(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidArgumentDomainException("Username cannot be null");
            }
            if(!Regex.IsMatch(value, _regex))
            {
                throw new InvalidArgumentDomainException($"Username {value} does not match with {_regex}");
            }

            Value = value;
        }

        public static implicit operator Username(string value) => new Username(value);
        public static implicit operator string(Username value) => value.Value;
    }
}
