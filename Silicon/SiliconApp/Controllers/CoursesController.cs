using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SiliconApp.Entities;
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
            var amountOfCourses = await _courseService.GetCourseCountAsync(categoryId, search); //Mängden kurser som ska finnas efter filtreringen, hämtas upp
            var amountPerPage = 9; //Här bestämmer man hur många kurser som ska synas på varje sida
            var numberOfPages = (int)Math.Ceiling(amountOfCourses / (double)amountPerPage); //Här kalkyleras hur många sidor det kommer bli

            var userSavedCourses = _userService.GetCourseList(userEntity);

            return View(new CoursesViewModel()
            {
                Courses = await _courseService.GetAllCoursesAsync(categoryId, search, currentPage, amountPerPage),
                Categories = await _categoryService.GetAllCategoriesAsync(),
                AmountPerPage = amountPerPage,
                NumberOfPages = numberOfPages,
                CurrentPage = currentPage,
                UserSavedCourses = userSavedCourses
            });
        }

        [Authorize]
        public async Task<IActionResult> SaveCourse(string id = "", string categoryId = "", string search = "", int currentPage = 1)
        {
            var userEntity = await _userService.GetUserEntityAsync(User);

            if (userEntity == null)
            {
                return RedirectToRoute(new { controller = "Account", action = "SignOut" });
            }

            var amountOfCourses = await _courseService.GetCourseCountAsync(categoryId, search);
            var amountPerPage = 9;
            var numberOfPages = (int)Math.Ceiling(amountOfCourses / (double)amountPerPage);

            var userSavedCourses = _userService.GetCourseList(userEntity);

            if (int.TryParse(id, out var intId))
            {
                var courseList = userSavedCourses.ToList();

                if (!courseList.Contains(intId)) //Om kurs Id't redan finns i listan så utförs inte koden inuti denna sats, eftersom den då inte ska läggas till igen
                {
                    courseList.Add(intId);
                    await _userService.UpdateCourseListAsync(userEntity, courseList); //Kursen har lagts till i kurs-listan, och user entiteten uppdateras med den nya listan

                    userSavedCourses = courseList;
                }

            }

            return View("Index", new CoursesViewModel()
            {
                Courses = await _courseService.GetAllCoursesAsync(categoryId, search, currentPage, amountPerPage),
                Categories = await _categoryService.GetAllCategoriesAsync(),
                AmountPerPage = amountPerPage,
                NumberOfPages = numberOfPages,
                CurrentPage = currentPage,
                UserSavedCourses = userSavedCourses
            });
        }

        [Authorize]
        public async Task<IActionResult> RemoveCourse(string id = "", string categoryId = "", string search = "", int currentPage = 1)
        {
            var userEntity = await _userService.GetUserEntityAsync(User);

            if (userEntity == null)
            {
                return RedirectToRoute(new { controller = "Account", action = "SignOut" });
            }

            var amountOfCourses = await _courseService.GetCourseCountAsync(categoryId, search);
            var amountPerPage = 9;
            var numberOfPages = (int)Math.Ceiling(amountOfCourses / (double)amountPerPage);

            var userSavedCourses = _userService.GetCourseList(userEntity);

            if (int.TryParse(id, out var intId))
            {
                var courseList = userSavedCourses.ToList();

                if (courseList.Remove(intId)) //Kursen försöks tas bort, och om kursen hittas / om den lyckas så utförs resten av koden i if satsen
                {
                    await _userService.UpdateCourseListAsync(userEntity, courseList);

                    userSavedCourses = courseList;
                }
            }

            return View("Index", new CoursesViewModel()
            {
                Courses = await _courseService.GetAllCoursesAsync(categoryId, search, currentPage, amountPerPage),
                Categories = await _categoryService.GetAllCategoriesAsync(),
                AmountPerPage = amountPerPage,
                NumberOfPages = numberOfPages,
                CurrentPage = currentPage,
                UserSavedCourses = userSavedCourses
                
            });
        }
    }
}
