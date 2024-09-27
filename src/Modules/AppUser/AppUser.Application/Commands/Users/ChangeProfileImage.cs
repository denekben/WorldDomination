using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace AppUser.Application.Commands.Users
{
    public sealed record ChangeProfileImage(Guid UserId, IFormFile FormFile) : IRequest;
}
