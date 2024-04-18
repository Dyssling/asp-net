using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SiliconApp.Services;
using SiliconApp.ViewModels;

namespace SiliconApp.Controllers
{
    public class CoursesController : Controller
    {
        private readonly UserService _userService;
        private readonly CourseService _courseService;
        private readonly CategoryService _categoryService;

        public CoursesController(UserService userService, CourseService courseService, CategoryService categoryService)
        {
            _userService = userService;
            _courseService = courseService;
            _categoryService = categoryService;
        }

        [Authorize]
        public async Task<IActionResult> Index(string categoryId = "", string search = "")
        {
            ViewData["Title"] = "Courses";

            var userEntity = await _userService.GetUserEntityAsync(User);

            if (userEntity == null)
            {
                return RedirectToRoute(new { controller = "Account", action = "SignOut" });
            }

            return View(new CoursesViewModel()
            {
                Courses = await _courseService.GetAllCoursesAsync(categoryId, search),
                Categories = await _categoryService.GetAllCategoriesAsync()
            });
        }
    }
}
