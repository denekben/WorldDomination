using Game.Application.Orders.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Game.WebUI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/countries")]
    public class CountryController : ControllerBase
    {
        private readonly ISender _sender;

        public CountryController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [Route("orders")]
        public async Task<IActionResult> SendOrder(SendOrder command)
        {
            await _sender.Send(command);
            return Ok();
        }
    }
}
