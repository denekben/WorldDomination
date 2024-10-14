using WorldDomination.Shared.Exceptions;

namespace User.Domain.Exceptions
{
    public class InvalidArgumentDomainException : WorldDominationException
    {
        public InvalidArgumentDomainException(string message) : base(message) { }
    }
}
