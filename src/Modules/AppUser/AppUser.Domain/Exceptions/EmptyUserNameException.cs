using Shared.Exceptions;

namespace AppUser.Domain.Exceptions
{
    public class EmptyUserNameException : WorldDominationException
    {
        public EmptyUserNameException() : base("UserName cannot be empty.")
        {
            
        }
    }
}
