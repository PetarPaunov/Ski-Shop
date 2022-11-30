using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SkiShop.Core.Contracts;
using SkiShop.Core.Contracts.Common;
using SkiShop.Core.Services;
using SkiShop.Core.Services.Common;

namespace SkiShop.Test.AdminServicesTests
{
    using CloudinaryDotNet;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using SkiShop.Core.Contracts.ProductContracts;
    using SkiShop.Core.Models.ProductViewModels;
    using SkiShop.Core.Services.ProductServices;
    using SkiShop.Data.Common;
    using SkiShop.Data.Models.Account;
    using SkiShop.Data.Models.Product;
    using SkiShop.Data.Models.ShoppingCart;
    using System.Text;

    public class ProductServiceAdminTests
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
        public async Task Check_If_AddNewProductAsync_Add_Correctly_Product_To_The_Database()
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg");

            var productModel = new AddProductViewModel()
            {
                Title = "Test Product",
                Description = "Test",
                Price = 323,
                BrandId = new Guid("a3beeab1-6e85-47bd-b62a-bc0bd2c681ee"),
                ModelId = new Guid("57f0de91-7fdd-4342-82cd-791841651ac9"),
                TypeId = new Guid("db927cb1-89fc-4887-b6b9-8ee3512eba05"),
                FrontImage = file,
                Quantity = 3
            };

            var productModel2 = new AddProductViewModel()
            {
                Title = "Test Product",
                Description = "Test",
                Price = 323,
                BrandId = new Guid("a3beeab1-6e85-47bd-b62a-bc0bd2c681ee"),
                ModelId = new Guid("57f0de91-7fdd-4342-82cd-791841651ac9"),
                TypeId = new Guid("db927cb1-89fc-4887-b6b9-8ee3512eba05"),
                FrontImage = file,
                Quantity = 3
            };

            Mock<ICommonService> commonMock = new Mock<ICommonService>();
            commonMock.Setup(x => x.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("TestImageUrl");

            var repo = serviceProvider.GetService<IRepository>();

            var service = new ProductServiceAdmin(repo, commonMock.Object);

            await service.AddNewProductAsync(productModel);
            await service.AddNewProductAsync(productModel2);

            var result = await repo.AllReadonly<Product>().ToListAsync();

            Assert.That(result.Count(), Is.EqualTo(2));
            commonMock.VerifyAll();
        }

        [Test]
        public async Task Check_If_GetAllBrandsAsync_Returns_All_Brands_In_The_Database()
        {
            Mock<ICommonService> commonMock = new Mock<ICommonService>();
            commonMock.Setup(x => x.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("TestImageUrl");

            var repo = serviceProvider.GetService<IRepository>();

            var service = new ProductServiceAdmin(repo, commonMock.Object);

            var result = await service.GetAllBrandsAsync();

            Assert.That(result.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task Check_If_GetAllModelsAsync_Returns_All_Models_In_The_Database()
        {
            Mock<ICommonService> commonMock = new Mock<ICommonService>();
            commonMock.Setup(x => x.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("TestImageUrl");

            var repo = serviceProvider.GetService<IRepository>();

            var service = new ProductServiceAdmin(repo, commonMock.Object);

            var result = await service.GetAllModelsAsync();

            Assert.That(result.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task Check_If_GetAllTypesAsync_Returns_All_Types_In_The_Database()
        {
            Mock<ICommonService> commonMock = new Mock<ICommonService>();
            commonMock.Setup(x => x.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("TestImageUrl");

            var repo = serviceProvider.GetService<IRepository>();

            var service = new ProductServiceAdmin(repo, commonMock.Object);

            var result = await service.GetAllTypesAsync();

            Assert.That(result.Count(), Is.EqualTo(3));
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

            var brand1 = new Brand()
            {
                Id = new Guid("a3beeab1-6e85-47bd-b62a-bc0bd2c681ee"),
                Name = "TestBrand1"
            };
            var brand2 = new Brand()
            {
                Name = "TestBrand2"
            };
            var brand3 = new Brand()
            {
                Name = "TestBrand3"
            };

            await repo.AddAsync(brand1);
            await repo.AddAsync(brand2);
            await repo.AddAsync(brand3);

            var type = new Type()
            {
                Id = new Guid("db927cb1-89fc-4887-b6b9-8ee3512eba05"),
                Name = "TestType1"
            };
            var type2 = new Type()
            {
                Id = new Guid(),
                Name = "TestType3"
            };
            var type3 = new Type()
            {
                Id = new Guid(),
                Name = "TestType3"
            };

            await repo.AddAsync(type);
            await repo.AddAsync(type2);
            await repo.AddAsync(type3);

            var model = new Model()
            {
                Id = new Guid("57f0de91-7fdd-4342-82cd-791841651ac9"),
                Name = "TestModel"
            };
            var model2 = new Model()
            {
                Name = "TestModel1"
            };
            var model3 = new Model()
            {
                Name = "TestModel2"
            };

            await repo.AddAsync(model);
            await repo.AddAsync(model2);
            await repo.AddAsync(model3);

            await repo.SaveChangesAsync();
        }
    }
}