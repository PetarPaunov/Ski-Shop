namespace SkiShop.Core.Services.Admin
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using SkiShop.Core.Contracts.Admin;
    using SkiShop.Core.Models.RoleViewModels;
    using SkiShop.Core.Models.UserViewModels;
    using SkiShop.Data.Common;
    using SkiShop.Data.Models.Account;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserServiceAdmin : IUserServiceAdmin
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserServiceAdmin(UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            var dataUsers = await userManager.Users.ToListAsync();

            var users = new List<UserViewModel>();

            foreach (var user in dataUsers)
            {
                var userRole = await userManager.GetRolesAsync(user);

                users.Add(new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = userRole
                });
            }
            return users;
        }

        public async Task<IEnumerable<UserEmailViewModel>> GetAllUserEmailsAsync()
        {
            var dataUsers = await userManager.Users.ToListAsync();

            var users = new List<UserEmailViewModel>();

            foreach (var user in dataUsers)
            {
                var userRole = await userManager.GetRolesAsync(user);

                users.Add(new UserEmailViewModel()
                {
                    Email = user.Email,
                });
            }
            return users;
        }
    }
}
