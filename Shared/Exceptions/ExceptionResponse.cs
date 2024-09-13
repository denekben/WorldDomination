using System.Net;

namespace Shared.Exceptions
{
    public sealed record ExceptionResponse(object Response, HttpStatusCode StatusCode);
}
