using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SiliconApp.Services;
using SiliconApp.ViewModels;

namespace SiliconApp.Controllers
{
    public class SingleCourseController : Controller
    {

        private readonly UserService _userService;
        private readonly CourseService _courseService;

        public SingleCourseController(UserService userService, CourseService courseService)
        {
            _userService = userService;
            _courseService = courseService;
        }


        [Authorize]
        public async Task<IActionResult> Index(int id)
        {
            //if (!_userService.IsUserSignedIn(User))
            //{
            //    return RedirectToRoute(new { controller = "Account", action = "SignIn" }); //Om användaren är utloggad redirectas man till Sign In sidan
            //}

            ViewData["Title"] = "Single Course";

            var userEntity = await _userService.GetUserEntityAsync(User);

            if (userEntity == null)
            {
                return RedirectToRoute(new { controller = "Account", action = "SignOut" });
            }

            var userSavedCourses = _userService.GetCourseList(userEntity);

            var course = await _courseService.GetOneCourseAsync(id);

            if (course != null)
            {
                return View(new SingleCourseViewModel()
                {
                    CourseEntity = course,
                    UserSavedCourses = userSavedCourses
                });
            }

            return RedirectToRoute(new { controller = "Error", action = "Index", statusCode = 404 }); //Om man fick tillbaka null från GetOneCourseAsync så omdirigeras man till NotFound sidan
        }

        [Authorize]
        public async Task<IActionResult> JoinCourse(string id)
        {
            var userEntity = await _userService.GetUserEntityAsync(User);

            if (userEntity == null)
            {
                return RedirectToRoute(new { controller = "Account", action = "SignOut" });
            }

            var userSavedCourses = _userService.GetCourseList(userEntity);

            if (int.TryParse(id, out var intId))
            {
                var courseList = userSavedCourses.ToList();

                if (!courseList.Contains(intId))
                {
                    courseList.Add(intId);
                    await _userService.UpdateCourseListAsync(userEntity, courseList);

                    userSavedCourses = courseList;
                }

            }

            var course = await _courseService.GetOneCourseAsync(intId);

            if (course != null)
            {
                return View("Index", new SingleCourseViewModel()
                {
                    CourseEntity = course,
                    UserSavedCourses = userSavedCourses
                });
            }

            return RedirectToRoute(new { controller = "Error", action = "Index", statusCode = 404 });
        }

        [Authorize]
        public async Task<IActionResult> LeaveCourse(string id)
        {
            var userEntity = await _userService.GetUserEntityAsync(User);

            if (userEntity == null)
            {
                return RedirectToRoute(new { controller = "Account", action = "SignOut" });
            }

            var userSavedCourses = _userService.GetCourseList(userEntity);

            if (int.TryParse(id, out var intId))
            {
                var courseList = userSavedCourses.ToList();

                if (courseList.Remove(intId))
                {
                    await _userService.UpdateCourseListAsync(userEntity, courseList);

                    userSavedCourses = courseList;
                }
            }

            var course = await _courseService.GetOneCourseAsync(intId);

            if (course != null)
            {
                return View("Index", new SingleCourseViewModel()
                {
                    CourseEntity = course,
                    UserSavedCourses = userSavedCourses
                });
            }

            return RedirectToRoute(new { controller = "Error", action = "Index", statusCode = 404 });
        }
    }
}
