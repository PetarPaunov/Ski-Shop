using SkiShop.Core.Models.RoleViewModels;

namespace SkiShop.Core.Contracts.Admin
{
	public interface IRoleServiceAdmin
	{
        Task<IEnumerable<RoleViewModel>> GetAllRolesAsync();
        Task CreateRoleAsync(string inputRole);
        Task AddToRoleAsync(string email, string role);
        Task DeleteRoleAsync(string name);
    }
}
