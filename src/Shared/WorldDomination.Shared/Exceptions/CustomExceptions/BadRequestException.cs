namespace WorldDomination.Shared.Exceptions.CustomExceptions
{
    public class BadRequestException : WorldDominationException
    {
        public BadRequestException() : base() { }

        public BadRequestException(string message) : base(message) { }

    }
}
