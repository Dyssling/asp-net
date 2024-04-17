using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiliconAPI.Filters;
using SiliconAPI.Models;
using SiliconAPI.Services;

namespace SiliconAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UseApiKey]
    public class SubscribersController : ControllerBase
    {
        private readonly SubscriberService _subscriberService;

        public SubscribersController(SubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(SubscriberModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _subscriberService.RegisterSubscriberAsync(model);

                if (result == "Success")
                {
                    return CreatedAtAction("Register", model);
                }

                else if (result == "Conflict")
                {
                    return Conflict();
                }
            }

            return BadRequest();
        }

        [HttpPost("Unregister")]
        public async Task<IActionResult> Unregister(string email)
        {
            var result = await _subscriberService.UnregisterSubscriberAsync(email);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
