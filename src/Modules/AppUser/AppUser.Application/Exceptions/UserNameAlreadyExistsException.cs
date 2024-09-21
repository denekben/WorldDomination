using Shared.Exceptions;

namespace AppUser.Application.Exceptions
{
    public class UserNameAlreadyExistsException : WorldDominationException
    {
        public UserNameAlreadyExistsException(string value) : base($"UserName \"{value}\" is already exists")
        {
        }
    }
}
