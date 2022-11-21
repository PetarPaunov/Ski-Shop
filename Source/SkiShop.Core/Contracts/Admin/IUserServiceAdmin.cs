namespace SkiShop.Core.Contracts.Admin
{
    using SkiShop.Core.Models.UserViewModels;
    using SkiShop.Core.Models.CommonViewModels;

    /// <summary>
    /// Administration service for user management
    /// </summary>
    public interface IUserServiceAdmin
	{
        /// <summary>
        /// Gets all users from the database
        /// </summary>
        /// <returns>A collection of users mapped to view model</returns>
		Task<IEnumerable<UserViewModel>> GetAllUsersAsync();

        /// <summary>
        /// Gets all user email addresses from the database
        /// </summary>
        /// <returns>A collection of email addresses mapped to view model</returns>
        Task<IEnumerable<UserEmailViewModel>> GetAllUserEmailsAsync();

        /// <summary>
        /// Gets all user orders from the database
        /// </summary>
        /// <returns>A collection of user orders mapped to a view model</returns>
        Task<IEnumerable<UserOrdersViewModel>> GetAllUserOrdersAsync();

        /// <summary>
        /// Finishes user order
        /// </summary>
        /// <param name="orderId">Identifier of the order</param>
        Task FinishUserOrderAsync(string orderId);
    }
}