﻿using Identity.Application.Commands;
using Identity.Application.Commands.Users;
using Identity.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebUI.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly ISender _sender;

        public AccountController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<UserIdentityDto>> RegisterAsync(RegisterNewUser command)
        {
            return Ok(await _sender.Send(command));
        }

        [HttpPost]
        [Route("sign-in")]
        public async Task<ActionResult<UserIdentityDto>> SignIn(SignIn command)
        {
            return Ok(await _sender.Send(command));
        }

        [HttpGet]
        [Route("refresh-token")]
        public async Task<ActionResult<string>> Refresh(RefreshExpiredToken command)
        {
            return Ok(await _sender.Send(command));
        }

        [HttpPut]
        [Route("username")]
        [Authorize]
        public async Task<ActionResult<string>> ChangeUsername(ChangeUsername command)
        {
            return Ok(await _sender.Send(command));  
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteAcount(DeleteUser command)
        {
            await _sender.Send(command);
            return Ok();
        }
    }
}
