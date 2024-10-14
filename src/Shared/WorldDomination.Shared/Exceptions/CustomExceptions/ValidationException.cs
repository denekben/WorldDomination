using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace WorldDomination.Shared.Exceptions.CustomExceptions
{
    public class ValidationException : WorldDominationException
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public ValidationException(IEnumerable<IdentityError> errors) : this()
        {
            Errors = errors
                .GroupBy(e => e.Code, e => e.Description)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

    }
}
