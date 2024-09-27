using AppUser.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace AppUser.Domain.ValueObjects
{
    public sealed record Email
    {
        public string Value { get; }
        private const string _regex = @"^[\w-\.]+@([\w -]+\.)+[\w-]{2,4}$";

        public Email(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidArgumentDomainException("Email cannot be null");
            }
            if (!Regex.IsMatch(value, _regex))
            {
                throw new InvalidArgumentDomainException($"Email {value} does not match with {_regex}");
            }

            Value = value;
        }

        public static implicit operator Email(string value) => new Email(value);
        public static implicit operator string(Email value) => value.Value;

        
    }
}
