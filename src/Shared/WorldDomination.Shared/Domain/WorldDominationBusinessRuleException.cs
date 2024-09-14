using Shared.Exceptions;

namespace Shared.Domain
{
    public class WorldDominationBusinessRuleException : WorldDominationException
    {
        public WorldDominationBusinessRuleException(string message) : base(message)
        {
        }
    }
}
