using MediatR;
using Microsoft.AspNetCore.Http;

namespace Users.Application.Users.Commands
{
    public sealed record UpdateCurrentUserImage(IFormFile FormFile) : IRequest;
}