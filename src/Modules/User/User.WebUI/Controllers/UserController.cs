using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Application.User.Queries;
using User.Shared.DTOs;
using Users.Application.Users.Commands;

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
        public async Task<ActionResult<List<SearchUserDto>?>> SearchUsers([FromQuery] SearchUsers query)
        {
            return Ok(await _sender.Send(query));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<Profile?>> GetProfileById([FromRoute] GetProfileById query)
        {
            return Ok(await _sender.Send(query));
        }

        [HttpGet]
        [Route("{id:guid}/achievments")]
        public async Task<ActionResult<List<UserAchievmentDto>?>> GetUserAchievmentById([FromRoute] GetUserAchievmentsById command)
        {
            return Ok(await _sender.Send(command));
        }
    }
}