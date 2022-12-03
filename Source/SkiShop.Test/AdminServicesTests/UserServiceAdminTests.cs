namespace SkiShop.Test.AdminServicesTests
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using SkiShop.Core.Services.Admin;
    using SkiShop.Data.Common;
    using SkiShop.Data.Models.Account;
    using SkiShop.Data.Models.Product;
    using SkiShop.Data.Models.ShoppingCart;

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
        public async Task GetAllUsersAsync_Should_Return_All_Users_In_The_Database()
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

        [Test]
        public async Task GetAllUserEmailsAsync_Should_Return_All_Users_Emails_From_The_Database()
        {
            var mockStore = Mock.Of<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>
                (mockStore, null, null, null, null, null, null, null, null);

            var repo = serviceProvider.GetService<IRepository>();

            var service = new UserServiceAdmin(mockUserManager.Object, repo);

            var result = await service.GetAllUserEmailsAsync();

            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(2));
                Assert.That(result.Any(x => x.Email == "Test@abv.bg"), Is.True);
                Assert.That(result.Any(x => x.Email == "Test@abv.bg3"), Is.False);
            });
        }

        [Test]
        public async Task GetAllUserOrdersAsync_Should_Return_All_Orders_In_The_Database()
        {
            var mockStore = Mock.Of<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>
                (mockStore, null, null, null, null, null, null, null, null);

            var repo = serviceProvider.GetService<IRepository>();

            var service = new UserServiceAdmin(mockUserManager.Object, repo);

            var result = await service.GetAllUserOrdersAsync();

            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(2));
                Assert.That(result.Any(x => x.UserName == "Test"), Is.True);
                Assert.That(result.Any(x => x.UserName == "Test2"), Is.False);
            });
        }

        [Test]
        public async Task FinishUserOrderAsync_Should_Delete_The_Order_From_The_Database()
        {
            var mockStore = Mock.Of<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>
                (mockStore, null, null, null, null, null, null, null, null);

            var repo = serviceProvider.GetService<IRepository>();

            var service = new UserServiceAdmin(mockUserManager.Object, repo);

            await service.FinishUserOrderAsync("4fc862b9-ef6a-4559-a69d-4149cd06fa34");

            var result = await service.GetAllUserOrdersAsync();

            Assert.That(result.Count(), Is.EqualTo(1));
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
                PhoneNumber = "0909090909"
            };

            user.ShoppingCart = new ShoppingCart() { UserId = "1" };

            var user2 = new ApplicationUser()
            {
                Id = "2",
                Email = "Test@abv.bg2",
                NormalizedEmail = "Test@abv.bg2",
                UserName = "Test2",
                NormalizedUserName = "TEST2",
                PhoneNumber = "0909090910"
            };

            user2.ShoppingCart = new ShoppingCart() { UserId = "2" };

            await repo.AddAsync(user);
            await repo.AddAsync(user2);

            var product = new Product()
            {
                Id = new Guid("55861159-5a03-4e86-a6b7-bfa5104f677d"),
                Brand = new Brand() { Name = "Test Brand" },
                Description = "Test Description",
                ImageUrl = "TestUrl",
                Price = 123,
                Title = "Test Product",
                Model = new Model() { Name = "Test Model" },
                Type = new Data.Models.Product.Type() { Name = "Ski" },
                Quantity = 3
            };

            await repo.AddAsync(product);

            var order = new Order()
            {
                Id = new Guid("4fc862b9-ef6a-4559-a69d-4149cd06fa34"),
                Product = product,
                Quantity = 3,
                ProductId = new Guid("55861159-5a03-4e86-a6b7-bfa5104f677d"),
                ApplicationUser = user,
                ApplicationUserId = "1"
            };

            var order2 = new Order()
            {
                Id = new Guid("93b4f4e7-cb23-4005-b287-945fef99612f"),
                Product = product,
                Quantity = 4,
                ProductId = new Guid("55861159-5a03-4e86-a6b7-bfa5104f677d"),
                ApplicationUser = user,
                ApplicationUserId = "1"
            };

            await repo.AddAsync(order);
            await repo.AddAsync(order2);

            await repo.SaveChangesAsync();
        }
    }
}