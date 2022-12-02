using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using SkiShop.Core.Contracts.Admin;
using SkiShop.Core.Services.Admin;

namespace SkiShop.Test.AdminServicesTests
{
    using Microsoft.Extensions.DependencyInjection;
    using SkiShop.Core.Contracts.ProductContracts;
    using SkiShop.Data.Common;
    using SkiShop.Data.Models.Account;
    using SkiShop.Data.Models.Product;
    using SkiShop.Data.Models.ShoppingCart;

    public class RoleServiceAdminTests
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
        public async Task CreateRoleAsync_Should_Add_New_Role_To_The_Database()
        {
            var repo = serviceProvider.GetRequiredService<IRepository>();

            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            Mock<RoleManager<IdentityRole>> mockRoleManager = new Mock<RoleManager<IdentityRole>>
                (roleStore.Object, null, null, null, null);
            // mockRoleManager.Setup(x => x.CreateAsync(It.IsAny<IdentityRole>()));

            var mockStore = Mock.Of<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>
                (mockStore, null, null, null, null, null, null, null, null);

            var service = new RoleServiceAdmin(mockRoleManager.Object, repo, mockUserManager.Object);

            await service.CreateRoleAsync("TestRole");
            await service.CreateRoleAsync("TestRole2");

            var result = await repo.AllReadonly<IdentityRole>().ToListAsync();

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

            await repo.AddAsync(user);
           
            await repo.SaveChangesAsync();
        }
    }
}
