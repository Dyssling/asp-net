using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SiliconApp.Entities;
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

        public async Task<IActionResult> Details()
        {
            if (!_userService.IsUserSignedIn(User))
            {
                return RedirectToRoute(new { controller = "Account", action = "SignIn" }); //Om användaren är utloggad redirectas man till Sign In sidan
            }

            ViewData["Active"] = "Details"; //För att man sedan ska kunna sätta en active klass på rätt knapp
            ViewData["Title"] = "Account Details";

            var userEntity = await _userService.GetUserEntityAsync(User);

            var viewModel = new AccountDetailsViewModel() //Här populerar jag min viewModel med värdena från den inloggade användaren, alltså userEntity.
            {
                BasicInfoForm = new AccountDetailsBasicInfoModel()
                {
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    Email = userEntity.Email!,
                    Phone = userEntity.PhoneNumber,
                    Bio = userEntity.Bio
                },

                AddressForm = new AccountDetailsAddressModel()
                {
                    Address1 = userEntity.Address?.Address1,
                    Address2 = userEntity.Address?.Address2,
                    PostalCode = userEntity.Address?.PostalCode,
                    City = userEntity.Address?.City
                },

                UserEntity = userEntity
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Details(AccountDetailsViewModel viewModel)
        {
            if (!_userService.IsUserSignedIn(User))
            {
                return RedirectToRoute(new { controller = "Account", action = "SignIn" }); //Kan hända att cookien går ut samtidigt som man gör en post, så därför är det bra att även här göra en inloggnings koll
            }

            ViewData["Active"] = "Details"; //För att man sedan ska kunna sätta en active klass på rätt knapp
            ViewData["Title"] = "Account Details";

            var userEntity = await _userService.GetUserEntityAsync(User);
            viewModel.UserEntity = new UserEntity() //Om jag INTE gör en NY entitet på detta viset så uppdateras informationen direkt i en och samma entitet på nåt jävla vis, och informationen i sidebaren uppdateras då även när den inte ska det, eftersom den självaste underliggande entiteten har uppdaterats antar jag???
            {
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Email = userEntity.Email
            };

            if (viewModel.BasicInfoForm.BasicInfoFormValue == "1") //Om BasicInfoFormValue är lika med 1 så innebär det att basicInfoSubmit() har körts, vilket alltså innebär att det är BasicInfo formuläret som har skickats.
            {
                viewModel.AddressForm = new AccountDetailsAddressModel() //Värdena i det andra formuläret måste populeras igen, eftersom de inte stannar kvar i viewModel när det görs en post från detta formuläret.
                {
                    Address1 = userEntity.Address?.Address1,
                    Address2 = userEntity.Address?.Address2,
                    PostalCode = userEntity.Address?.PostalCode,
                    City = userEntity.Address?.City
                };

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                userEntity.FirstName = viewModel.BasicInfoForm.FirstName;
                userEntity.LastName = viewModel.BasicInfoForm.LastName;
                userEntity.Email = viewModel.BasicInfoForm.Email;
                userEntity.PhoneNumber = viewModel.BasicInfoForm.Phone;
                userEntity.Bio = viewModel.BasicInfoForm.Bio;

                string message = await _userService.UpdateUserAsync(userEntity);

                if (message == "Success!")
                {
                    return RedirectToRoute(new { controller = "Account", action = "Details" });
                }

                ViewData["BasicInfoErrorMessage"] = message; //Om användaren inte kunde uppdateras så skrivs felmeddelandet ut på sidan.

                return View(viewModel);
            }

            else if (viewModel.AddressForm.AddressFormValue == "1") //Om det däremot är AddressFormValue som är lika med 1, så är det Address formuläret som har skickats.
            {
                viewModel.BasicInfoForm = new AccountDetailsBasicInfoModel()
                {
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    Email = userEntity.Email!,
                    Phone = userEntity.PhoneNumber,
                    Bio = userEntity.Bio
                };

                //Dessa ModelState rader är för att göra alla required fält till Valid i det andra formuläret, och även ta bort dess felmeddelanden från modellen (vet inte om det behövs eftersom dessa fält ändå kommer populeras)
                ModelState["BasicInfoForm.FirstName"]!.ValidationState = ModelValidationState.Valid;
                ModelState["BasicInfoForm.FirstName"]!.Errors.Clear();
                ModelState["BasicInfoForm.LastName"]!.ValidationState = ModelValidationState.Valid;
                ModelState["BasicInfoForm.LastName"]!.Errors.Clear();
                ModelState["BasicInfoForm.Email"]!.ValidationState = ModelValidationState.Valid;
                ModelState["BasicInfoForm.Email"]!.Errors.Clear();

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                if (userEntity.Address == null) //Om userEntity inte har någon address entitet så skapas en ny med den angivna informationen.
                {
                    userEntity.Address = new AddressEntity()
                    {
                        Address1 = viewModel.AddressForm.Address1,
                        Address2 = viewModel.AddressForm.Address2,
                        PostalCode = viewModel.AddressForm.PostalCode,
                        City = viewModel.AddressForm.City
                    };
                }

                else
                {
                    userEntity.Address.Address1 = viewModel.AddressForm.Address1;
                    userEntity.Address.Address2 = viewModel.AddressForm.Address2;
                    userEntity.Address.PostalCode = viewModel.AddressForm.PostalCode;
                    userEntity.Address.City = viewModel.AddressForm.City;
                }

                string message = await _userService.UpdateUserAddressInfoAsync(userEntity);

                if (message == "Success!")
                {
                    return RedirectToRoute(new { controller = "Account", action = "Details" });
                }

                ViewData["AddressErrorMessage"] = message; //Om användaren inte kunde uppdateras så skrivs felmeddelandet ut på sidan.

                return View(viewModel);
            }

            return RedirectToRoute(new { controller = "Account", action = "Details" });
        }

        public async Task<IActionResult> Security()
        {
            if (!_userService.IsUserSignedIn(User))
            {
                return RedirectToRoute(new { controller = "Account", action = "SignIn" });
            }

            ViewData["Active"] = "Security";
            ViewData["Title"] = "Account Security";

            var userEntity = await _userService.GetUserEntityAsync(User);

            var viewModel = new AccountSecurityViewModel()
            {
                UserEntity = userEntity
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Security(AccountSecurityViewModel viewModel)
        {
            if (!_userService.IsUserSignedIn(User))
            {
                return RedirectToRoute(new { controller = "Account", action = "SignIn" }); //Kan hända att cookien går ut samtidigt som man gör en post, så därför är det bra att även här göra en inloggnings koll
            }

            ViewData["Active"] = "Security";
            ViewData["Title"] = "Account Security";

            var userEntity = await _userService.GetUserEntityAsync(User);
            viewModel.UserEntity = new UserEntity()
            {
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Email = userEntity.Email
            };

            if (viewModel.PasswordForm.PasswordFormValue == "1")
            {
                ModelState["DeleteAccountForm.ConfirmDelete"]!.ValidationState = ModelValidationState.Valid;
                ModelState["DeleteAccountForm.ConfirmDelete"]!.Errors.Clear();

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                string message = await _userService.UpdateUserPasswordAsync(userEntity, viewModel.PasswordForm.CurrentPassword, viewModel.PasswordForm.NewPassword);

                if (message == "Success!")
                {
                    ViewData["PasswordSuccessMessage"] = "Your password has been updated!";

                    return View(viewModel);
                }

                ViewData["PasswordErrorMessage"] = message;

                return View(viewModel);
            }

            else if (viewModel.DeleteAccountForm.DeleteAccountFormValue == "1")
            {
                ModelState["PasswordForm.CurrentPassword"]!.ValidationState = ModelValidationState.Valid;
                ModelState["PasswordForm.CurrentPassword"]!.Errors.Clear();
                ModelState["PasswordForm.NewPassword"]!.ValidationState = ModelValidationState.Valid;
                ModelState["PasswordForm.NewPassword"]!.Errors.Clear();
                ModelState["PasswordForm.ConfirmNewPassword"]!.ValidationState = ModelValidationState.Valid;
                ModelState["PasswordForm.ConfirmNewPassword"]!.Errors.Clear();

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                string message = await _userService.DeleteUserAsync(userEntity);

                if (message == "Success!")
                {
                    return RedirectToRoute(new { controller = "Account", action = "SignOut" });
                }

                ViewData["DeleteAccountErrorMessage"] = message;

                return View(viewModel);
            }

            return RedirectToRoute(new { controller = "Account", action = "Security" });
        }

        public async Task<IActionResult> SavedCourses()
        {
            if (!_userService.IsUserSignedIn(User))
            {
                return RedirectToRoute(new { controller = "Account", action = "SignIn" }); //Om användaren är utloggad redirectas man till Sign In sidan
            }

            ViewData["Active"] = "SavedCourses";
            ViewData["Title"] = "Saved Courses";

            var userEntity = await _userService.GetUserEntityAsync(User);

            var viewModel = new AccountSavedCoursesViewModel()
            {
                UserEntity = userEntity
            };

            return View(viewModel);
        }

        public IActionResult SignIn()
        {
            if (_userService.IsUserSignedIn(User))
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
            if (_userService.IsUserSignedIn(User))
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

        public new async Task<IActionResult> SignOut()
        {
            if (!_userService.IsUserSignedIn(User))
            {
                return RedirectToRoute(new { controller = "Account", action = "SignIn" });
            }

            bool result = await _userService.SignOutUserAsync();

            if (result)
            {
                return RedirectToRoute(new { controller = "Account", action = "SignIn" });
            }

            return RedirectToRoute(new { controller = "Account", action = "Details" });
        }
    }
}
