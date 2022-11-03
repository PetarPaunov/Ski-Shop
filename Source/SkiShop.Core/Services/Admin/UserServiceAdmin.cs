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
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IRepository repository;


        public UserServiceAdmin(UserManager<ApplicationUser> _userManager,
                                RoleManager<IdentityRole> _roleManager,
                                IRepository _repository)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            repository = _repository;
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

        public async Task CreateRoleAsync(string inputRole)
        {
            var role = new IdentityRole(inputRole);
            await roleManager.CreateAsync(role);
        }

        public async Task AddToRoleAsync(string email, string role)
        {
            var user = await userManager.FindByEmailAsync(email);

            await userManager.AddToRoleAsync(user, role);
        }

        public async Task<IEnumerable<RoleViewModel>> GetAllRolesAsync()
        {
            var roles = await repository.All<IdentityRole>()
                .Select(x => new RoleViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            return roles;
        }
    }
}
