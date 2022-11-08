using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkiShop.Core.Contracts.Admin;
using SkiShop.Core.Models.RoleViewModels;
using SkiShop.Data.Common;
using SkiShop.Data.Models.Account;

namespace SkiShop.Core.Services.Admin
{
	public class RoleServiceAdmin : IRoleServiceAdmin
	{
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IRepository repository;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleServiceAdmin(RoleManager<IdentityRole> _roleManager, 
               IRepository _repository, 
               UserManager<ApplicationUser> _userManager)
        {
            this.roleManager = _roleManager;
            this.repository = _repository;
            this.userManager = _userManager;
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

        public async Task DeleteRoleAsync(string name)
        {
            var role = await roleManager.Roles.FirstOrDefaultAsync(x => x.Name == name);

            await roleManager.DeleteAsync(role);
        }
    }
}
