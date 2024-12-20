﻿using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using WorldDomination.Shared.Exceptions.CustomExceptions;

namespace WorldDomination.Shared.Services
{
    public class HttpContextService : IHttpContextService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HttpContextService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid GetCurrentUserId()
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new NotFoundException("Cannot find user");
            }
            return new Guid(userId);
        }
    }
}