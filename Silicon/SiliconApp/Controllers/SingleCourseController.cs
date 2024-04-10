﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiliconApp.Services;

namespace SiliconApp.Controllers
{
    public class SingleCourseController : Controller
    {

        private readonly UserService _userService;

        public SingleCourseController(UserService userService)
        {
            _userService = userService;
        }


        [Authorize]
        public async Task<IActionResult> Index()
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

            return View();
        }
    }
}
