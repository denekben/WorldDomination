using WorldDomination.Shared.Exceptions;

namespace WorldDomination.Shared.Exceptions.CustomExceptions
{
    public class InvalidArgumentDomainException : WorldDominationException
    {
        public InvalidArgumentDomainException() : base() { }
        public InvalidArgumentDomainException(string message) : base(message) { }
    }
}
