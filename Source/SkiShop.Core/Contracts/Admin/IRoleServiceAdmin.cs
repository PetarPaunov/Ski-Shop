namespace SkiShop.Core.Contracts.Admin
{
    using SkiShop.Core.Models.RoleViewModels;

    /// <summary>
    /// Administrator services for managing roles
    /// </summary>
    public interface IRoleServiceAdmin
	{
        /// <summary>
        /// Gets all existing roles from the database
        /// </summary>
        /// <returns>A collection of roles mapped to a view model</returns>
        Task<IEnumerable<RoleViewModel>> GetAllRolesAsync();

        /// <summary>
        /// Creates a new role 
        /// </summary>
        /// <param name="inputRole">Name of the role</param>
        Task CreateRoleAsync(string inputRole);

        /// <summary>
        /// Changes user role
        /// </summary>
        /// <param name="email">Email of the user</param>
        /// <param name="role">Name of the role</param>
        Task AddToRoleAsync(string email, string role);
        
        /// <summary>
        /// Deletes a role from the database
        /// </summary>
        /// <param name="name">Name of the role</param>
        Task DeleteRoleAsync(string name);
    }
}