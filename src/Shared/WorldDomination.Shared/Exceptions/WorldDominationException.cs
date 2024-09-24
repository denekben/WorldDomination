using System;

namespace Shared.Exceptions
{
    public abstract class WorldDominationException : Exception
    {
        protected WorldDominationException() : base() { }
        protected WorldDominationException(string message) : base(message){ }
        protected WorldDominationException(string message, Exception ex) : base(message, ex) { }
    }
}
