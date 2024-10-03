using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUser.Application.Exceptions
{
    public class BadRequestException : WorldDominationException
    {
        public BadRequestException() : base() { }

        public BadRequestException(string message) : base(message) { }

    }
}
