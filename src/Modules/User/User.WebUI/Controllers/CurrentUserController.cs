using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Application.User.Queries;
using User.Shared.DTOs;
using Users.Application.Users.Commands;

namespace User.WebUI.Controllers
{
    [ApiController]
   // [Authorize]
    [Route("user")]
    public class CurrentUserController : ControllerBase
    {
        private readonly ISender _sender;

        public CurrentUserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Profile?>> GetCurrentUserProfileById([FromRoute] GetCurrentUserProfile query)
        {
            return Ok(await _sender.Send(query));
        }

        [HttpGet]
        [Route("achievments")]
        public async Task<ActionResult<List<UserAchievmentDto>?>> GetCurrentUserAchievment([FromRoute] GetCurrentUserAchievments command)
        {
            return Ok(await _sender.Send(command));
        }

        [HttpPut]
        [Route("image")]
        public async Task<IActionResult> UpdateCurrentUserProfileImage([FromForm] UpdateCurrentUserImage command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpPut]
        [Route("info")]
        public async Task<IActionResult> UpdateCurrentUserProfileInfo(UpdateCurrentUserInfo command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpDelete]
        [Route("image")]
        public async Task<IActionResult> DeleteCurrentUserProfileImage(DeleteCurrentUserImage command)
        {
            await _sender.Send(command);
            return Ok();
        }
    }
}
