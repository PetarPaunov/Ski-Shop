namespace SkiShop.Core.Contracts.Admin
{
    using SkiShop.Core.Models.CommonViewModels;
    using SkiShop.Core.Models.UserViewModels;

    public interface IUserServiceAdmin
	{
		Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
        Task<IEnumerable<UserEmailViewModel>> GetAllUserEmailsAsync();
        Task<IEnumerable<UserOrdersViewModel>> GetAllUserOrdersAsync();
        Task FinishUserOrderAsync(string orderId);
    }
}