using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiliconAPI.Models;
using SiliconAPI.Entities;
using SiliconAPI.Services;
using SiliconAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;

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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCourses(string category = "", string search = "")
        {
            IEnumerable<CourseEntity> list;

            //if ((category.IsNullOrEmpty() || category == "0") && search.IsNullOrEmpty()) //Om ingen kategori är angiven, eller om den angivna kategorin är 0, OCH om söktermen är tom
            //{
            //    list = await _courseService.GetAllCoursesAsync(); //Så får man alla kurser

            //    return Ok(list);
            //}

            search = Uri.UnescapeDataString(search); //Mellanslag och andra specialtecken brukar "escapas" i en URL, och bli andra kombinationer av tecken istället. Här unescapas de igen
            search = search.Replace("+", " "); //UnescapeDataString omvandlar inte plustecken till mellanslag (mellanslag kan ibland omvandlas till plustecken i URLen), så här görs det manuellt

            list = await _courseService.GetAllCoursesFilteredAsync(category, search);

            return Ok(list);
        }

        [HttpGet("GetOne/{id:int}")]
        public async Task<IActionResult> GetOneCourse(int id)
        {
            var entity = await _courseService.GetOneCourseAsync(id);

            if (entity != null)
            {
                return Ok(entity);
            }

            return NotFound();
        }
    }
}
