using Shared.Exceptions;
using System;

namespace AppUser.Application.Exceptions
{
    public class UserNotFoundByIdException : WorldDominationException
    {
        public UserNotFoundByIdException(Guid id) : base($"User was not found by ID: {id}.")
        {
            
        }
    }
}
