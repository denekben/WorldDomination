using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Application.Users.Commands;
using User.Application.Users.Queries;
using User.Shared.DTOs;

namespace User.WebUI.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [Route("profile/{id:guid}")]
        public async Task<ActionResult<UserDto>> GetUserProfileById([FromRoute] GetUserProfileById command)
        {
            return Ok(await _sender.Send(command));
        }

        [HttpPut]
        [Route("profile/image")]
        [Authorize]
        public async Task<IActionResult> ChangeProfileImage([FromForm] ChangeProfileImage command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpPut]
        [Route("profile/info")]
        [Authorize]
        public async Task<IActionResult> ChangeProfileInfo(ChangeProfileInfo command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpDelete]
        [Route("profile/image")]
        [Authorize]
        public async Task<IActionResult> DeleteProfileImage(DeleteProfileImage command)
        {
            await _sender.Send(command);
            return Ok();
        }
    }
}