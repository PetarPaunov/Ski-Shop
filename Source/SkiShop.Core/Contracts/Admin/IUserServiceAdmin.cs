namespace SkiShop.Core.Contracts.Admin
{
    using SkiShop.Core.Models.RoleViewModels;
    using SkiShop.Core.Models.UserViewModels;

    public interface IUserServiceAdmin
	{
		Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
        Task<IEnumerable<RoleViewModel>> GetAllRolesAsync();
        Task CreateRoleAsync(string inputRole);
        Task AddToRoleAsync(string email, string role);
    }
}
