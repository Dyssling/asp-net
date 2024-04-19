﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SiliconApp.Entities;
using SiliconApp.Models;
using SiliconApp.Repositories;
using SiliconApp.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace SiliconApp.Services
{
    public class UserService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserRepository _userRepository;
        private readonly AddressRepository _addressRepository;

        public UserService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, UserRepository userRepository, AddressRepository addressRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
        }

        public async Task<string> CreateNewUserAsync(SignUpModel model)
        {
            try
            {
                var exists = await _userManager.Users.AnyAsync(x => x.Email == model.Email); //Här får man true om en användare med samma email finns, annars false

                if (exists)
                {
                    return "A user with the same email already exists.";
                }

                var userEntity = new UserEntity() //Annars skapas en entitet med den angivna användarinformationen
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email
                };

                var result = await _userManager.CreateAsync(userEntity, model.Password); //Sedan försöker man skapa användaren i databasen ihop med lösenordet.

                if (result.Succeeded) //Om det lyckades så får man ett success meddelande.
                {
                    return "Success!";
                }
            }

            catch { }

            return "An error occurred while creating the user."; //Om något annat skulle gå snett så får man ett error meddelande.

        }

        public async Task<UserEntity> GetUserEntityAsync(ClaimsPrincipal user)
        {
            try
            {
                var userEntity = await _userManager.GetUserAsync(user); //Här hämtas en userEntity med hjälp av userManager. User claims skickas alltså in, en entitet returneras.
                userEntity = await _userRepository.GetOneAsync(x => x == userEntity); //Samma entitet hämtas igen, fast genom _userRepository där man även får addressinformationen.

                return userEntity;
            }
            catch { }

            return null!;

        }

        public async Task<string> UpdateUserAsync(UserEntity userEntity)
        {
            try
            {
                var user = await _userRepository.GetOneAsync(x => x.Email == userEntity.Email); //Här får man ut en userEntity om en sådan med samma email redan finns

                if (user != null && user.Id != userEntity.Id) //Om den hämtade entitetens id inte är samma som den givna entitetens id (vilket det inte kommer vara om det är två olika användare)
                {
                    return "A user with the same email already exists.";
                }

                var userResult = await _userManager.UpdateAsync(userEntity);
                var userNameResult = await _userManager.SetUserNameAsync(userEntity, userEntity.Email); //Man måste även ändra användarnamnet i denna separata userManager metod

                if (userResult.Succeeded && userNameResult.Succeeded)
                {
                    return "Success!";
                }
            }

            catch { }

            return "An error occurred while updating the user.";
        }

        public async Task<string> UpdateUserAddressInfoAsync(UserEntity userEntity)
        {
            try
            {
                var userResult = await _userManager.UpdateAsync(userEntity);

                if (userResult.Succeeded)
                {
                    return "Success!";
                }
            }

            catch { }

            return "An error occurred while updating the user.";
        }

        public async Task<string> UpdateUserPasswordAsync(UserEntity userEntity, string currentPassword, string newPassword)
        {
            try
            {
                var passwordResult = await _userManager.ChangePasswordAsync(userEntity, currentPassword, newPassword);

                if (passwordResult.Succeeded)
                {
                    return "Success!";
                }

                return "Your current password was incorrect.";
            }

            catch { }

            return "An error occurred while updating your password.";
        }

        public async Task<string> DeleteUserAsync(UserEntity userEntity)
        {
            try
            {
                var userResult = await _userManager.DeleteAsync(userEntity);
                await _addressRepository.DeleteAsync(x => x.Id == userEntity.AddressId); //När användaren tas bort så tas även addressen bort, vars id är samma som användarens AddressId. Om användaren inte har någon address entitet, så får man false tillbaka, vilket kvittar i detta fallet. Dock kan det även innebära att något gick snett, så i verkliga scenarion bör man göra någon typ av extra check

                if (userResult.Succeeded)
                {
                    return "Success!";
                }
            }

            catch { }

            return "An error occurred while deleting your account.";
        }

       public async Task<string> SignInUserAsync(SignInModel model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false); //Här försöker man logga in användaren med den angivna användarinformationen.

                if (result.Succeeded) //Om man lyckades får man ett success meddelande
                {
                    return "Success!";
                }

                return "Incorrect email or password."; //Annars innebär det att kombinationen var fel
            }

            catch { }

            return "An error occurred while attempting to sign in."; //Övrigt fel

        }

        public bool IsUserSignedIn(ClaimsPrincipal user) //Metoden returnerar true om användaren är inloggad, annars false
        {
            if (_signInManager.IsSignedIn(user))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> SignOutUserAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }

            catch { }
            return false;
        }

        public AuthenticationProperties ExternalAuthProps(string provider)
        {
            try
            {
                return _signInManager.ConfigureExternalAuthenticationProperties(provider, $"/account/{provider}callback");
            }

            catch { }

            return null!;
        }

        public async Task<string> SignInExternalUserAsync(ExternalLoginInfo info)
        {
            try
            {
                if (info != null)
                {
                    var userEntity = new UserEntity() //Skapa en ny entitet utav den externa informationen
                    {
                        Id = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier)!,
                        FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                        LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)! ?? "", //Om last name skulle vara null så blir det en tom string istället så att den ändå kan läggas in i databasen i detta fallet
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                        UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                        IsExternal = true
                    };



                    var user = await _userRepository.GetOneAsync(x => x.Id == userEntity.Id); //Hämta en eventuell användare med samma Id (för att sedan kolla om användaren finns i databasen)

                    if (user == null) //Om användaren inte finns i databasen
                    {
                        var userWithSameEmail = await _userRepository.GetOneAsync(x => x.Email == userEntity.Email); //Hämta en eventuell användare med samma email

                        if (userWithSameEmail != null) //Om det redan finns en användare med samma email i databasen så får man ett felmeddelande
                        {
                            return "A user with the same email already exists.";
                        }

                        var result = await _userManager.CreateAsync(userEntity); //Annars skapas användaren i databasen

                        if (result.Succeeded)
                        {
                            user = await _userRepository.GetOneAsync(x => x.Id == userEntity.Id); //Hämta upp den nya användaren för att sedan utföra lite saker
                        }

                    }

                    if (user != null) //En extra check för att se om användaren finns vid detta laget
                    {
                        if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email || !user.IsExternal) //Om någon av denna information har ändrats externt
                        {
                            var userWithSameEmail = await _userRepository.GetOneAsync(x => x.Email == userEntity.Email); //Hämta återigen en eventuell användare med samma email

                            if (userWithSameEmail != null && userWithSameEmail.Id != userEntity.Id) //Om det finns en användare med samma email MEN ett annat Id, så innebär det att det finns en ANNAN användare med samma email i databasen
                            {
                                return "A user with the new email already exists.";
                            }

                            user.FirstName = userEntity.FirstName; //Annars uppdateras informationen
                            user.LastName = userEntity.LastName;
                            user.Email = userEntity.Email;
                            user.UserName = userEntity.Email;
                            user.IsExternal = true;

                            await _userManager.UpdateAsync(user);
                        }

                        await _signInManager.SignInAsync(user, true); //Logga slutligen in användaren

                        return "Success!";
                    }
                }
            }

            catch (Exception error)
            {
                Debug.WriteLine(error);
            }

            return "Failed to authenticate user.";
        }

        public async Task<bool> UpdateCourseListAsync(UserEntity userEntity, List<int> courseList)
        {
            try
            {
                string jsonList = null!;

                if (!courseList.IsNullOrEmpty())
                {
                    jsonList = JsonConvert.SerializeObject(courseList);
                }

                userEntity.CourseList = jsonList;

                await _userManager.UpdateAsync(userEntity);

                return true;
            }

            catch { }

            return false;
        }

        public IEnumerable<int> GetCourseList(UserEntity userEntity)
        {
            try
            {
                var jsonList = userEntity.CourseList;
                IEnumerable<int> courseList = new List<int>();

                if (!jsonList.IsNullOrEmpty())
                {
                    courseList = JsonConvert.DeserializeObject<IEnumerable<int>>(jsonList!)!;
                }

                return courseList;
            }

            catch { }

            return new List<int>(); //En tom lista returneras om något gick snett

        }
    }
}
