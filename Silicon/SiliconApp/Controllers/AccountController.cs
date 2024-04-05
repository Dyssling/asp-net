using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SiliconApp.Models;
using SiliconApp.ViewModels;

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

            if (viewModel.BasicInfoForm.TestValue == "1")
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" }); //TODO: Här kan du göra en modelstate remove eller nåt skit
            }

            else if (viewModel.AddressForm.TestValue == "1")
            {
                return RedirectToRoute(new { controller = "Account", action = "SignUp" });
            }

            return RedirectToRoute(new { controller = "Account", action = "SignIn" });
        }

        //[HttpPost]
        //public IActionResult DetailsAddressForm(AccountDetailsViewModel viewModel)
        //{
            

        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return RedirectToAction(nameof(Details), viewModel);
        //    //}

        //    //return RedirectToAction(nameof(Details));
        //}

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
