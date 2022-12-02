using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SkiShop.Core.Contracts.Admin;
using SkiShop.Core.Models.UserViewModels;
using SkiShop.Core.Services.Admin;
using SkiShop.Data.Common;
using SkiShop.Data.Models.Account;
using SkiShop.Data.Models.ShoppingCart;

namespace SkiShop.Test.AdminServicesTests
{
    public class UserServiceAdminTests
    {
        private IServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IRepository, Repository>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IRepository>();

            await SeedDbAsync(repo);
        }

        [Test]
        public async Task GetAllUsersAsync_Should_Return_All_Users_In_The_Database2()
        {
            var userRoles = new List<string>() { "TestRole" };

            var mockStore = Mock.Of<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>
                (mockStore, null, null, null, null, null, null, null, null);

            mockUserManager.Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(userRoles);


            var repo = serviceProvider.GetService<IRepository>();

            var service = new UserServiceAdmin(mockUserManager.Object, repo);

            var result = await service.GetAllUsersAsync();

            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IRepository repo)
        {
            var user = new ApplicationUser()
            {
                Id = "1",
                Email = "Test@abv.bg",
                NormalizedEmail = "Test@abv.bg",
                UserName = "Test",
                NormalizedUserName = "TEST",
            };

            user.ShoppingCart = new ShoppingCart() { UserId = "1" };

            var user2 = new ApplicationUser()
            {
                Id = "2",
                Email = "Test@abv.bg2",
                NormalizedEmail = "Test@abv.bg2",
                UserName = "Test2",
                NormalizedUserName = "TEST2",
            };

            user2.ShoppingCart = new ShoppingCart() { UserId = "2" };

            await repo.AddAsync(user);
            await repo.AddAsync(user2);

            await repo.SaveChangesAsync();
        }
    }
}