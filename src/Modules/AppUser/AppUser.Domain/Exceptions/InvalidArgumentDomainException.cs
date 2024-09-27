using Shared.Exceptions;

namespace AppUser.Domain.Exceptions
{
    public class InvalidArgumentDomainException : WorldDominationException
    {
        public InvalidArgumentDomainException(string message) : base(message) { }
    }
}
