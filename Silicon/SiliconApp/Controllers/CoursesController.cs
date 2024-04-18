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
        public async Task<IActionResult> Index(string categoryId = "", string search = "", int currentPage = 1) //Att sätta default värden på parametrarna innebär att de är valfria
        {
            ViewData["Title"] = "Courses";

            var userEntity = await _userService.GetUserEntityAsync(User);

            if (userEntity == null)
            {
                return RedirectToRoute(new { controller = "Account", action = "SignOut" });
            }
            var amountOfCourses = await _courseService.GetCourseCountAsync(categoryId, search);
            var amountPerPage = 3;
            var numberOfPages = (int)Math.Ceiling(amountOfCourses / (double)amountPerPage);

            return View(new CoursesViewModel()
            {
                Courses = await _courseService.GetAllCoursesAsync(categoryId, search, currentPage, amountPerPage),
                Categories = await _categoryService.GetAllCategoriesAsync(),
                AmountPerPage = amountPerPage,
                NumberOfPages = numberOfPages,
                CurrentPage = currentPage
            });
        }
    }
}
