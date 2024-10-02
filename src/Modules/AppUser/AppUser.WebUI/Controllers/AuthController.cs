using AppUser.Application.Commands.Auth;
using AppUser.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppUser.WebUI.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthController(ISender sender)
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
        [Route("signin")]
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
    }
}
