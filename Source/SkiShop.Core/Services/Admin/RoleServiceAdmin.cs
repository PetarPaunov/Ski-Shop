namespace SkiShop.Core.Services.Admin
{
    using SkiShop.Data.Common;
    using SkiShop.Data.Models.Account;
    using SkiShop.Core.Contracts.Admin;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using SkiShop.Core.Models.RoleViewModels;

    using static SkiShop.Core.Constants.ExeptionMessagesConstants;

    /// <summary>
    /// Administrator services for managing roles
    /// </summary>
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

        /// <summary>
        /// Creates a new role 
        /// </summary>
        /// <param name="inputRole">Name of the role</param>
        public async Task CreateRoleAsync(string inputRole)
        {
            var role = new IdentityRole(inputRole);
            await repository.AddAsync(role);
            await repository.SaveChangesAsync();
        }

        /// <summary>
        /// Changes user role
        /// </summary>
        /// <param name="email">Email of the user</param>
        /// <param name="role">Name of the role</param>
        public async Task AddToRoleAsync(string email, string role)
        {
            var user = await userManager.FindByEmailAsync(email);

            var isInRole = await userManager.IsInRoleAsync(user, role);

            if (!isInRole)
            {
                var currentRoles = await userManager.GetRolesAsync(user);

                await userManager.RemoveFromRolesAsync(user, currentRoles);

                await userManager.AddToRoleAsync(user, role);
            }
        }

        /// <summary>
        /// Gets all existing roles from the database
        /// </summary>
        /// <returns>A collection of roles mapped to a view model</returns>
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

        /// <summary>
        /// Deletes a role from the database
        /// </summary>
        /// <param name="name">Name of the role</param>
        public async Task DeleteRoleAsync(string name)
        {
            var role = await repository
                .AllReadonly<IdentityRole>()
                .FirstOrDefaultAsync(x => x.Name == name);

            if (role == null)
            {
                throw new ArgumentNullException(RoleNotFound);
            }

            await roleManager.DeleteAsync(role);
        }
    }
}