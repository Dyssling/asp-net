﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SiliconApp.Models;
using SiliconApp.Services;
using SiliconApp.ViewModels;
using System.Diagnostics;
using System.Globalization;

namespace SiliconApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Details()
        {
            ViewData["Active"] = "Details"; //För att man sedan ska kunna sätta en active klass på rätt knapp
            ViewData["Title"] = "Account Details";

            return View(new AccountDetailsViewModel());
            
        }

        [HttpPost]
        public IActionResult Details(AccountDetailsViewModel viewModel)
        {
            ViewData["Active"] = "Details"; //För att man sedan ska kunna sätta en active klass på rätt knapp
            ViewData["Title"] = "Account Details";

            if (viewModel.BasicInfoForm.BasicInfoFormValue == "1") //Om BasicInfoFormValue är lika med 1 så innebär det att basicInfoSubmit() har körts, vilket alltså innebär att det är BasicInfo formuläret som har skickats.
            {
                foreach (var property in typeof(AccountDetailsAddressModel).GetProperties()) //Bing Copilot hjälpte mig bygga denna foreach loopen. Den tar bort varje fält i det andra formuläret från ModelState, så att bägge formulär inte valideras på samma gång. 
                {
                    var fieldName = property.Name; // Get the field name
                    var fullKey = $"AddressForm.{fieldName}"; // Construct the full key

                    // Remove field
                    ModelState.Remove(fullKey);
                }

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                return RedirectToRoute(new { controller = "Account", action = "Details" }); 
            }

            else if (viewModel.AddressForm.AddressFormValue == "1") //Om det däremot är AddressFormValue som är lika med 1, så är det Address formuläret som har skickats.
            {
                foreach (var property in typeof(AccountDetailsBasicInfoModel).GetProperties())
                {
                    var fieldName = property.Name; // Get the field name
                    var fullKey = $"BasicInfoForm.{fieldName}"; // Construct the full key

                    // Remove field
                    ModelState.Remove(fullKey);
                }

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                return RedirectToRoute(new { controller = "Account", action = "Details" });
            }

            return RedirectToRoute(new { controller = "Account", action = "Details" });
        }

        public IActionResult Security()
        {
            ViewData["Active"] = "Security";
            ViewData["Title"] = "Account Security";

            return View(new AccountSecurityViewModel());
        }

        [HttpPost]
        public IActionResult Security(AccountSecurityViewModel viewModel)
        {
            ViewData["Active"] = "Security";
            ViewData["Title"] = "Account Security";

            if (viewModel.PasswordForm.PasswordFormValue == "1")
            {
                foreach (var property in typeof(AccountSecurityDeleteAccountModel).GetProperties())
                {
                    var fieldName = property.Name; // Get the field name
                    var fullKey = $"DeleteAccountForm.{fieldName}"; // Construct the full key

                    // Remove field
                    ModelState.Remove(fullKey);
                }

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                return RedirectToRoute(new { controller = "Account", action = "Security" });
            }

            else if (viewModel.DeleteAccountForm.DeleteAccountFormValue == "1")
            {
                foreach (var property in typeof(AccountSecurityPasswordModel).GetProperties())
                {
                    var fieldName = property.Name; // Get the field name
                    var fullKey = $"PasswordForm.{fieldName}"; // Construct the full key

                    // Remove field
                    ModelState.Remove(fullKey);
                }

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                return RedirectToRoute(new { controller = "Account", action = "SignIn" });
            }

            return RedirectToRoute(new { controller = "Account", action = "Security" });
        }

        public IActionResult SavedCourses()
        {
            ViewData["Active"] = "SavedCourses";
            ViewData["Title"] = "Saved Courses";

            return View();
        }

        public IActionResult SignIn()
        {
            if (User != null) //Om användaren är inloggad kommer User objektet inte vara null
            {
                return RedirectToRoute(new { controller = "Account", action = "Details" }); //Om användaren är inloggad redirectas man till Account details
            }

            ViewData["Title"] = "Sign In";

            return View(new SignInViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel viewModel)
        {
            ViewData["Title"] = "Sign In";

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            string message = await _userService.SignInUserAsync(viewModel.Form); //Här körs SignInUserAsync från UserService, och meddelandet lagras.

            if (message == "Success!")
            {
                return RedirectToRoute(new { controller = "Account", action = "Details" });
            }

            ViewData["ErrorMessage"] = message; //Om man inte lyckades logga in så skrivs felmeddelandet ut på sidan.

            return View(viewModel);
        }

        public IActionResult SignUp()
        {
            if (User != null)
            {
                return RedirectToRoute(new { controller = "Account", action = "Details" });
            }

            ViewData["Title"] = "Sign Up";

            return View(new SignUpViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
        {
            ViewData["Title"] = "Sign Up";

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            string message = await _userService.CreateNewUserAsync(viewModel.Form); //Här körs CreateNewUserAsync från UserService, och meddelandet lagras.

            if(message == "Success!")
            {
                return RedirectToRoute(new { controller = "Account", action = "SignIn" });
            }

            ViewData["ErrorMessage"] = message; //Om användaren inte kunde skapas så skrivs felmeddelandet ut på sidan.

            return View(viewModel);
            
        }
    }
}
