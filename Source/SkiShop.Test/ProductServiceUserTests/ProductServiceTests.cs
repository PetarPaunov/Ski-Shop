namespace SkiShop.Test.ProductServiceUserTests
{
    using Microsoft.Extensions.DependencyInjection;
    using SkiShop.Core.Contracts.ProductContracts;
    using SkiShop.Core.Services.ProductServices;
    using SkiShop.Data.Common;
    using SkiShop.Data.Models.Account;
    using SkiShop.Data.Models.Product;
    using SkiShop.Data.Models.ShoppingCart;
    using static SkiShop.Core.Constants.ExeptionMessagesConstants;

    public class ProductServiceTests
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
                .AddSingleton<IProductService, ProductService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IRepository>();

            await SeedDbAsync(repo);
        }

        [Test]
        public void AddNewComment_Method_Should_Throw_ArgumentNullException_If_The_Product_Is_Not_Found()
        {
            var service = serviceProvider.GetService<IProductService>();

            Assert.CatchAsync<ArgumentNullException>(async ()
                => await service.AddNewComment("Test Comment", "55861159-5a03-4e86-a6b7-bfa5104f677a", "1"),
                ProductNotFound);
        }

        [Test]
        public void AddNewComment_Method_Should_Throw_ArgumentNullException_If_The_User_Is_Not_Found()
        {
            var service = serviceProvider.GetService<IProductService>();

            Assert.CatchAsync<ArgumentNullException>(async ()
                => await service.AddNewComment("Test Comment", "55861159-5a03-4e86-a6b7-bfa5104f677d", "2"),
                UserNotFound);
        }

        [Test]
        public void AddNewComment_Method_Should_Not_Throw_ArgumentNullException_If_Correct_Data_Is_Passed()
        {
            var service = serviceProvider.GetService<IProductService>();

            Assert.DoesNotThrowAsync(async ()
                => await service.AddNewComment("Test Comment", "55861159-5a03-4e86-a6b7-bfa5104f677d", "1"));
        }

        [Test]
        public async Task GetAllProductsAsync_Method_Should_Return_Correct_Data()
        {
            var service = serviceProvider.GetService<IProductService>();

            var result = await service.GetAllProductsAsync(1, 1);

            Assert.That(result.TotalProductsCount, Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllProductsAsync_Method_Should_Return_Correct_Data_When_Search_Term_Is_Passed()
        {
            var service = serviceProvider.GetService<IProductService>();

            var result = await service.GetAllProductsAsync(1, 1, null, "Test");

            var product = result.Products.FirstOrDefault();

            Assert.That(result.TotalProductsCount, Is.EqualTo(1));
            Assert.That(product.Title, Is.EqualTo("Test Product"));
        }

        [Test]
        public async Task GetAllProductsAsync_Method_Should_Return_Correct_Data_When_Specific_Type_Is_Passed()
        {
            var service = serviceProvider.GetService<IProductService>();

            var result = await service.GetAllProductsAsync(1, 1, "Snowboard", null);

            var product = result.Products.FirstOrDefault();

            Assert.That(result.TotalProductsCount, Is.EqualTo(1));
            Assert.That(product.Title, Is.EqualTo("Snowboard"));
        }

        [Test]
        public async Task GetFirstEightProductsAsync_Method_Should_Return_Correct_Data()
        {
            var service = serviceProvider.GetService<IProductService>();

            var result = await service.GetFirstEightProductsAsync();

            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetProductByIdAsync_Method_Should_Throw_ArgumentNullException_When_Product_Is_Not_Found()
        {
            var service = serviceProvider.GetService<IProductService>();

            Assert.CatchAsync(async ()
                => await service.GetProductByIdAsync("55861159-5a03-4e86-a6b7-bfa5104f676z"),
                ProductNotFound);
        }

        [Test]
        public async Task GetProductByIdAsync_Method_Should_Return_The_Correct_Product()
        {
            var service = serviceProvider.GetService<IProductService>();

            var result = await service.GetProductByIdAsync("55861159-5a03-4e86-a6b7-bfa5104f677d");

            Assert.That(result.Id, Is.EqualTo("55861159-5a03-4e86-a6b7-bfa5104f677d"));
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
                Model = new Model() { Name = "Test Model" },
                Type = new Type() { Name = "Ski" },
                Quantity = 3
            };

            var secondProduct = new Product()
            {
                Id = new Guid("55861159-5a03-4e86-a6b7-bfa5104f677e"),
                Brand = new Brand() { Name = "TATATAT" },
                Description = "SDGRV",
                ImageUrl = "URLRYRYTl",
                Price = 123,
                Title = "Snowboard",
                Model = new Model() { Name = "DFBFG" },
                Type = new Type() { Name = "Snowboard" },
                Quantity = 3
            };

            await repo.AddAsync(product);
            await repo.AddAsync(secondProduct);

            await repo.SaveChangesAsync();
        }
    }
}
