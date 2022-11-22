namespace SkiShop.Core.Services.Admin
{
    using SkiShop.Data.Common;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using SkiShop.Data.Models.Account;
    using SkiShop.Core.Contracts.Admin;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using SkiShop.Data.Models.ShoppingCart;
    using SkiShop.Core.Models.UserViewModels;
    using SkiShop.Core.Models.CommonViewModels;

    /// <summary>
    /// Administration service for user management
    /// </summary>
    public class UserServiceAdmin : IUserServiceAdmin
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository repository;

        public UserServiceAdmin(UserManager<ApplicationUser> _userManager, 
                                IRepository _repository)
        {
            userManager = _userManager;
            repository = _repository;
        }

        /// <summary>
        /// Gets all users from the database
        /// </summary>
        /// <returns>A collection of users mapped to view model</returns>
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

        /// <summary>
        /// Gets all user email addresses from the database
        /// </summary>
        /// <returns>A collection of email addresses mapped to view model</returns>
        public async Task<IEnumerable<UserEmailViewModel>> GetAllUserEmailsAsync()
        {
            var dataUsers = await userManager.Users.ToListAsync();

            var emails = new List<UserEmailViewModel>();

            foreach (var user in dataUsers)
            {
                var userRole = await userManager.GetRolesAsync(user);

                emails.Add(new UserEmailViewModel()
                {
                    Email = user.Email,
                });
            }

            return emails;
        }

        /// <summary>
        /// Gets all user orders from the database
        /// </summary>
        /// <returns>A collection of user orders mapped to a view model</returns>
        public async Task<IEnumerable<UserOrdersViewModel>> GetAllUserOrdersAsync()
        {
            var orders = await repository.All<Order>()
                .Select(x => new UserOrdersViewModel()
                {
                    OrderId = x.Id.ToString(),
                    UserName = x.ApplicationUser.UserName,
                    Email = x.ApplicationUser.Email,
                    PhoneNumber = x.ApplicationUser.PhoneNumber,
                    ProductTitle = x.Product.Title,
                    ProductPrice = x.Product.Price.ToString(),
                    ImageUrl = x.Product.ImageUrl,
                    Quantity = x.Quantity
                })
                .ToListAsync();

            return orders;
        }

        /// <summary>
        /// Finishes user order
        /// </summary>
        /// <param name="orderId">Identifier of the order</param>
        public async Task FinishUserOrderAsync(string orderId)
        {
            var orderGuidId = new Guid(orderId);

            await repository.DeleteAsync<Order>(orderGuidId);
            await repository.SaveChangesAsync();
        }
    }
}