using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiliconAPI.Models;
using SiliconAPI.Entities;
using SiliconAPI.Services;
using SiliconAPI.Filters;
using Microsoft.AspNetCore.Authorization;

namespace SiliconAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UseApiKey]
    public class CoursesController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CoursesController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> CreateCourse(CourseModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _courseService.CreateNewCourseAsync(model);

                if (result)
                {
                    return CreatedAtAction("CreateCourse", model);
                }
            }

            return BadRequest();
        }

        [HttpPost("Update/{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateCourse(CourseModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _courseService.UpdateCourseAsync(model, id);

                if (result)
                {
                    return Ok();
                }
            }

            return NotFound();
        }

        [HttpPost("Delete/{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateCourse(int id)
        {

            var result = await _courseService.DeleteCourseAsync(id);

            if (result)
            {
                return Ok();
            }


            return NotFound();
        }
    }
}
