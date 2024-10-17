using Identity.Shared.DTOs;
using MediatR;
using System;

namespace Identity.Application.Commands.Users
{
    public sealed record ChangeUsername(string Username) : IRequest<string>;
}
