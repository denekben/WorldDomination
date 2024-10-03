using Humanizer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;

namespace Shared.Exceptions
{
    internal sealed class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        private static readonly ConcurrentDictionary<Type, string> Codes = new();

        public ExceptionResponse Map(Exception exception)
        {
            if(exception is WorldDominationException ex)
            {
                if(ex.Errors.Count > 0)
                {
                    var errors = new List<Error>();

                    foreach (var kvp in ex.Errors)
                    {
                        foreach (var errorMessage in kvp.Value)
                        {
                            errors.Add(new Error(GetErrorCode(ex), errorMessage));
                        }
                    }

                    return new ExceptionResponse(new ErrorsResponse(errors.ToArray()), HttpStatusCode.BadRequest);
                } 
                else
                {
                    return new ExceptionResponse(new ErrorsResponse(new Error(GetErrorCode(ex), ex.Message)), HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return new ExceptionResponse(new ErrorsResponse(new Error("error", "There was an error.")), HttpStatusCode.InternalServerError);
            }
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
