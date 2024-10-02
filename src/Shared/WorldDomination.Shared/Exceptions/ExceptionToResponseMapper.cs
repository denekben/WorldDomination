using Humanizer;
using System;
using System.Collections.Concurrent;
using System.Net;

namespace Shared.Exceptions
{
    internal sealed class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        private static readonly ConcurrentDictionary<Type, string> Codes = new();

        public ExceptionResponse Map(Exception exception)
        {
            return new ExceptionResponse(new ErrorsResponse(new Error(GetErrorCode(exception), exception.Message))
                , HttpStatusCode.BadRequest);

        }

        private record Error(string Code, string Message);

        private record ErrorsResponse(params Error[] Errors);

        private static string GetErrorCode(object exception)
        {
            var type = exception.GetType();
            return Codes.GetOrAdd(type, type.Name.Underscore().Replace("_exception", string.Empty));
        }
    }
}
