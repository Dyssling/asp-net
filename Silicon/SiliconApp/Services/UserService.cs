using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiliconApp.Entities;
using SiliconApp.Models;
using SiliconApp.Repositories;
using SiliconApp.ViewModels;
using System.Security.Claims;

namespace SiliconApp.Services
{
    public class UserService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserRepository _userRepository;

        public UserService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, UserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
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

            }

            catch { }

            return "An error occurred while updating your password.";
        }

       public async Task<string> SignInUserAsync(SignInModel model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false); //Här försöker man logga in användaren med den angivna användarinformationen.

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
    }
}
