using Identity.Application.Commands.Auth;
using Identity.Application.Commands.Users;
using Identity.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebUI.Controllers
{
    [ApiController]
    [Route("account")]
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
        [Route("signin")]
        public async Task<ActionResult<UserIdentityDto>> SignIn(SignIn command)
        {
            return Ok(await _sender.Send(command));
        }

        [HttpGet]
        [Route("refresh-token")]
        public async Task<ActionResult<string>> Refresh([FromQuery] RefreshExpiredToken command)
        {
            return Ok(await _sender.Send(command));
        }

        [HttpPut]
        [Route("username")]
        [Authorize]
        public async Task<ActionResult<UserIdentityDto>> ChangeUsername(ChangeUsername command)
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
