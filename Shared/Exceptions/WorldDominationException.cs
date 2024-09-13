using System;

namespace Shared.Exceptions
{
    public abstract class WorldDominationException : Exception
    {
        protected WorldDominationException(string message) : base(message)
        {

        }
    }
}
