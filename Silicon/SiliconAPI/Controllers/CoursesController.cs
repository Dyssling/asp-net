using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiliconAPI.Models;
using SiliconAPI.Entities;
using SiliconAPI.Services;
using SiliconAPI.Filters;

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

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _courseService.CreateNewCourseAsync(model);

                if (result)
                {
                    return Created();
                }
            }

            return BadRequest();
        }
    }
}
