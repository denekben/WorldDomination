using Game.Application.Orders.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Game.WebUI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("countries")]
    public class CountryController : ControllerBase
    {
        private readonly ISender _sender;

        public CountryController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [Route("orders")]
        public IActionResult ApplyOrder()
        {
            return Ok();
        }
    }
}
