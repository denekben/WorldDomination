using Game.Application.DTOs;
using Game.Application.UseCases.Rooms.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Game.WebUI.Controllers
{
    [ApiController]
    [Route("api/rooms")]
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
    }
}
