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
        public async Task<IActionResult> Index(string categoryId)
        {
            ViewData["Title"] = "Courses";

            var userEntity = await _userService.GetUserEntityAsync(User);

            if (userEntity == null)
            {
                return RedirectToRoute(new { controller = "Account", action = "SignOut" });
            }

            var test = await _courseService.GetAllCoursesAsync(categoryId);

            if (!categoryId.IsNullOrEmpty())
            {
                return View(new CoursesViewModel()
                {
                    Courses = await _courseService.GetAllCoursesAsync(categoryId),
                    Categories = await _categoryService.GetAllCategoriesAsync()
                });
            }

            return View(new CoursesViewModel()
            {
                Courses = await _courseService.GetAllCoursesAsync("0"), //Om categoryId parametern skulle vara tom av någon anledning så blir den 0 istället (alla kategorier kommer visas)
                Categories = await _categoryService.GetAllCategoriesAsync()
            });

        }
    }
}
