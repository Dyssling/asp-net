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
    public class ContactController : ControllerBase
    {
        private readonly ContactService _contactService;

        public ContactController(ContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost("CreateRequest")]
        public async Task<IActionResult> CreateRequest(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _contactService.CreateContactRequestAsync(model);

                if (result)
                {
                    return CreatedAtAction("CreateRequest", model);
                }
            }

            return BadRequest();
        }
    }
}
