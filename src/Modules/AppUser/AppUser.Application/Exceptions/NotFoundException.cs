using Shared.Exceptions;
using System;

namespace AppUser.Application.Exceptions
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