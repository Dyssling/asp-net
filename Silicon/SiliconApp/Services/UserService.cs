using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiliconApp.Entities;
using SiliconApp.Models;
using SiliconApp.ViewModels;

namespace SiliconApp.Services
{
    public class UserService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public UserService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
    }
}
