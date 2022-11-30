namespace SkiShop.Test.ShoppingCartServiceTests
{
    using NUnit.Framework;
    using SkiShop.Data.Common;
    using SkiShop.Data.Models.Account;
    using SkiShop.Data.Models.Product;
    using SkiShop.Data.Models.ShoppingCart;
    using SkiShop.Core.Services.ShoppingCart;
    using SkiShop.Core.Contracts.ShoppingCart;
    using Microsoft.Extensions.DependencyInjection;

    public class ShoppingCartTests
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
                .AddSingleton<IShoppingCartService, ShoppingCartService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IRepository>();

            await SeedDbAsync(repo);
        }

        [Test]
        public void Throws_ArgumentNullException_When_The_Product_Is_Not_Found()
        {
            var service = serviceProvider.GetService<IShoppingCartService>();

            Assert.CatchAsync<ArgumentNullException>(async () 
                => await service.
                    AddProductInShoppingCartAsync("55861159-5a03-4e86-a6b7-bfa5104f677e", "1", 1)
                    , "The product was not found in the database");
        }

        [Test]
        public void Throws_ArgumentNullException_When_The_User_Is_Not_Found()
        {
            var service = serviceProvider.GetService<IShoppingCartService>();

            Assert.CatchAsync<ArgumentNullException>(async ()
                => await service.
                    AddProductInShoppingCartAsync("55861159-5a03-4e86-a6b7-bfa5104f677d", "2", 1)
                    , "The user was not found in the database");
        }

        [Test]
        public void Should_Pass_When_The_Passed_Data_Is_Correct()
        {
            var service = serviceProvider.GetService<IShoppingCartService>();

            Assert.DoesNotThrowAsync(async ()
                => await service.
                    AddProductInShoppingCartAsync("55861159-5a03-4e86-a6b7-bfa5104f677d", "1", 1));
        }

        [Test]
        public async Task User_Should_Have_One_Product_In_The_Cart()
        {
            var service = serviceProvider.GetService<IShoppingCartService>();

            await service.AddProductInShoppingCartAsync("55861159-5a03-4e86-a6b7-bfa5104f677d", "1", 1);

            var result = await service.AllShoppingCartProductsAsync("1");

            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task User_Should_Not_Have_Any_Products_In_The_Cart()
        {
            var service = serviceProvider.GetService<IShoppingCartService>();

            var result = await service.AllShoppingCartProductsAsync("1");

            Assert.Zero(result.Count());
        }

        [Test]
        public async Task Count_Of_The_Products_Inside_The_Users_Cart_Should_Be_Zero()
        {
            var service = serviceProvider.GetService<IShoppingCartService>();

            var result = await service.CartProductsCoutAsync("1");

            Assert.Zero(result);
        }

        [Test]
        public async Task Count_Of_The_Products_Inside_The_Users_Cart_Should_Be_One()
        {
            var service = serviceProvider.GetService<IShoppingCartService>();

            await service.AddProductInShoppingCartAsync("55861159-5a03-4e86-a6b7-bfa5104f677d", "1", 1);

            var result = await service.CartProductsCoutAsync("1");

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Should_Throw_ArgumentNullException_When_The_User_Is_Not_Found_When_Placing_Order()
        {
            var service = serviceProvider.GetService<IShoppingCartService>();

            Assert.CatchAsync<ArgumentNullException>(async () 
                => await service.PlaceUserOrderAsync("2"),
                    "The user was not found in the database");
        }

        [Test]
        public async Task Should_Pass_If_The_Order_Is_Added_Successfully()
        {
            var service = serviceProvider.GetService<IShoppingCartService>();

            await service.AddProductInShoppingCartAsync("55861159-5a03-4e86-a6b7-bfa5104f677d", "1", 1);

            Assert.DoesNotThrowAsync(async ()
                => await service.PlaceUserOrderAsync("1"));
        }

        [Test]
        public void Should_Throw_ArgumentNullException_When_The_User_Is_Not_Found_When_Deleting_From_Cart()
        {
            var service = serviceProvider.GetService<IShoppingCartService>();

            Assert.CatchAsync<ArgumentNullException>(async ()
                => await service.RemoveFromCartAsync("55861159-5a03-4e86-a6b7-bfa5104f677d", "2"),
                    "The user was not found in the database");
        }

        [Test]
        public async Task Should_Pass_If_The_User_Is_Found_When_Deleting_From_Cart()
        {
            var service = serviceProvider.GetService<IShoppingCartService>();

            await service.AddProductInShoppingCartAsync("55861159-5a03-4e86-a6b7-bfa5104f677d", "1", 1);

            Assert.DoesNotThrowAsync(async ()
                => await service.RemoveFromCartAsync("55861159-5a03-4e86-a6b7-bfa5104f677d", "1"));
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

            var product = new Product()
            {
                Id = new Guid("55861159-5a03-4e86-a6b7-bfa5104f677d"),
                Brand = new Brand() { Name = "Test Brand" },
                Description = "Test Description",
                ImageUrl = "TestUrl",
                Price = 123,
                Title = "Test Product",
                Model = new Model() { Name = "Test Model"},
                Type = new Type() { Name = "Test Type"},
                Quantity = 3
            };

            await repo.AddAsync(product);

            await repo.SaveChangesAsync();
        }
    }
}