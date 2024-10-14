using System;
using System.Collections.Generic;

namespace WorldDomination.Shared.Exceptions
{
    public abstract class WorldDominationException : Exception
    {
        protected WorldDominationException() : base()
        {
            Errors = new Dictionary<string, string[]>();
        }
        protected WorldDominationException(string message) : base(message)
        {
            Errors = new Dictionary<string, string[]>();
        }
        public IDictionary<string, string[]> Errors { get; protected set; }
    }
}
