using MediatR;
using System;
using System.Collections.Generic;
using User.Shared.DTOs;

namespace User.Application.User.Queries
{
    public sealed record GetUserAchievmentsById(Guid id) : IRequest<List<UserAchievmentDto>?>;
}
