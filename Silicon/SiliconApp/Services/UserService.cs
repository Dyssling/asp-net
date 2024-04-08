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

        public UserService(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> CreateNewUserAsync(SignUpModel model)
        {
            try
            {
                var exists = await _userManager.Users.AnyAsync(x => x.Email == model.Email);

                if (exists)
                {
                    return "A user with the same email already exists.";
                }

                var userEntity = new UserEntity()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email
                };

                var result = await _userManager.CreateAsync(userEntity, model.Password);

                if (result.Succeeded)
                {
                    return "Success!";
                }
            }

            catch { }

            return "An error occurred while creating the user.";

        }
    }
}
