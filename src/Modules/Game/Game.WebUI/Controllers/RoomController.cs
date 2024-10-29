using Game.Application.Rooms.Commands;
using Game.Application.Rooms.Queries;
using Game.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Game.WebUI.Controllers
{
    [ApiController]
    [Route("rooms")]
    public class RoomController : ControllerBase
    {
        private readonly ISender _sender;

        public RoomController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<List<RoomDto>>> SearchRooms([FromQuery] SearchRooms query)
        {
            return Ok( await _sender.Send(query));
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateRoom(CreateRoom command)
        {
            return Ok(await _sender.Send(command));
        }
    }
}
