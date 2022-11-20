namespace SkiShop.Core.Services.Admin
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using SkiShop.Core.Contracts.Admin;
    using SkiShop.Core.Models.CommonViewModels;
    using SkiShop.Core.Models.RoleViewModels;
    using SkiShop.Core.Models.UserViewModels;
    using SkiShop.Data.Common;
    using SkiShop.Data.Models.Account;
    using SkiShop.Data.Models.ShoppingCart;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserServiceAdmin : IUserServiceAdmin
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository repository;

        public UserServiceAdmin(UserManager<ApplicationUser> _userManager, IRepository _repository)
        {
            userManager = _userManager;
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

        public async Task FinishUserOrderAsync(string orderId)
        {
            var orderGuidId = new Guid(orderId);

            await repository.DeleteAsync<Order>(orderGuidId);
            await repository.SaveChangesAsync();
        }
    }
}
