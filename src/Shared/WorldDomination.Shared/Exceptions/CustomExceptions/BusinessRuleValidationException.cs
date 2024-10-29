namespace WorldDomination.Shared.Exceptions.CustomExceptions
{
    public class BusinessRuleValidationException : WorldDominationException
    {
        public BusinessRuleValidationException() : base() { }
        public BusinessRuleValidationException(string message) : base(message) { }
    }
}
