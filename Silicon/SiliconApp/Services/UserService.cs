using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiliconApp.Entities;
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

        public async Task<string> CreateNewUserAsync(SignUpViewModel viewModel)
        {
            var exists = await _userManager.Users.AnyAsync(x => x.Email == viewModel.Form.Email);

            if (exists)
            {
                return "A user with the same email already exists.";
            }

            var userEntity = new UserEntity()
            {
                FirstName = viewModel.Form.FirstName,
                LastName = viewModel.Form.LastName,
                Email = viewModel.Form.Email,
                UserName = viewModel.Form.Email
            };

            var result = await _userManager.CreateAsync(userEntity, viewModel.Form.Password);

            if (result.Succeeded)
            {
                return "Success!";
            }

            return "An error occurred while creating the user.";

        }
    }
}
