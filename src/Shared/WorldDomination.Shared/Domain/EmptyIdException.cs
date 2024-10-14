using WorldDomination.Shared.Exceptions;

namespace WorldDomination.Shared.Domain
{
    public class EmptyIdException : WorldDominationException
    {
        public EmptyIdException() : base("Id cannot be empty.") { }
    }
}
