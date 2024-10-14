namespace WorldDomination.Shared.Exceptions.CustomExceptions
{
    public class NotFoundException : WorldDominationException
    {
        public NotFoundException() : base()
        {

        }

        public NotFoundException(string message) : base(message)
        {

        }
    }
}