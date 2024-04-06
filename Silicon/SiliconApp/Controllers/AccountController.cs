﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SiliconApp.Models;
using SiliconApp.ViewModels;
using System.Globalization;

namespace SiliconApp.Controllers
{
    public class AccountController : Controller
    {
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

            if (viewModel.BasicInfoForm.BasicInfoFormValue == "1")
            {
                foreach (var property in typeof(AccountDetailsAddressModel).GetProperties()) //Bing Copilot hjälpte mig bygga denna loopen. Den tar bort varje fält i det andra formuläret från ModelState, så att bägge formulär inte valideras på samma gång. 
                {
                    var fieldName = property.Name; // Get the field name
                    var fullKey = $"AddressForm.{fieldName}"; // Construct the full key

                    // Remove field
                    ModelState.Remove(fullKey);
                }

                //foreach (var property in typeof(AccountDetailsBasicInfoModel).GetProperties())
                //{
                //    var fieldName = property.Name; // Get the field name
                //    var fullKey = $"BasicInfoForm.{fieldName}"; // Construct the full key

                //    // Retrieve the user-entered value from the view model
                //    var userEnteredValue = property.GetValue(viewModel.BasicInfoForm);

                //    // Set the model value with the user-entered value
                //    ModelState.SetModelValue(fullKey, new ValueProviderResult(userEnteredValue?.ToString(), CultureInfo.InvariantCulture));
                //}

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            else if (viewModel.AddressForm.AddressFormValue == "1")
            {
                foreach (var property in typeof(AccountDetailsBasicInfoModel).GetProperties())
                {
                    var fieldName = property.Name; // Get the field name
                    var fullKey = $"BasicInfoForm.{fieldName}"; // Construct the full key

                    // Remove field
                    ModelState.Remove(fullKey);
                }

                //foreach (var property in typeof(AccountDetailsAddressModel).GetProperties())
                //{
                //    var fieldName = property.Name; // Get the field name
                //    var fullKey = $"AddressForm.{fieldName}"; // Construct the full key

                //    // Retrieve the user-entered value from the view model
                //    var userEnteredValue = property.GetValue(viewModel.AddressForm);

                //    // Set the model value with the user-entered value
                //    ModelState.SetModelValue(fullKey, new ValueProviderResult(userEnteredValue?.ToString(), CultureInfo.InvariantCulture));
                //}

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return RedirectToRoute(new { controller = "Account", action = "SignIn" }); //HÄR SKA DU ÄNDRA SEN TILL NÅN ANNAN SIDA, har bara denna nu när jag testar
        }

        public IActionResult Security()
        {
            ViewData["Active"] = "Security";
            ViewData["Title"] = "Account Security";

            return View();
        }

        public IActionResult SavedCourses()
        {
            ViewData["Active"] = "SavedCourses";
            ViewData["Title"] = "Saved Courses";

            return View();
        }

        public IActionResult SignIn()
        {
            ViewData["Title"] = "Sign In";

            return View(new SignInViewModel());
        }

        [HttpPost]
        public IActionResult SignIn(SignInViewModel viewModel)
        {
            ViewData["Title"] = "Sign In";

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        public IActionResult SignUp()
        {
            ViewData["Title"] = "Sign Up";

            return View(new SignUpViewModel());
        }

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel viewModel)
        {
            ViewData["Title"] = "Sign Up";

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            return RedirectToRoute(new { controller = "Account", action = "SignIn" });
        }
    }
}
