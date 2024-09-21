using Shared.Exceptions;

namespace AppUser.Domain.Exceptions
{
    public class InvalidUserNameLengthException : WorldDominationException
    {
        public InvalidUserNameLengthException(int value) : base($"UserName length must be greater than 1 and less than 15 symbols. Current length {value}.")
        {
        }
    }
}
