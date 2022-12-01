namespace SkiShop.Test.AdminServicesTests
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using SkiShop.Core.Contracts.Common;
    using SkiShop.Core.Models.ProductViewModels;
    using SkiShop.Core.Services;
    using SkiShop.Data.Common;
    using SkiShop.Data.Models.Account;
    using SkiShop.Data.Models.Product;
    using SkiShop.Data.Models.ShoppingCart;
    using System.Text;
    using static SkiShop.Core.Constants.ExeptionMessagesConstants;

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

            Assert.That(result.Count(), Is.EqualTo(3));
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

        [Test]
        public void DeleteProductAsync_Should_Throw_ArgumentNullException_When_The_Product_Does_Not_Exist()
        {
            Mock<ICommonService> commonMock = new Mock<ICommonService>();
            commonMock.Setup(x => x.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("TestImageUrl");

            var repo = serviceProvider.GetService<IRepository>();

            var service = new ProductServiceAdmin(repo, commonMock.Object);

            Assert.CatchAsync<ArgumentNullException>(async ()
                    => await service.DeleteProductAsync("693310f0-1205-4104-b410-f3702f000fa7"),
                ProductNotFound);
        }

        [Test]
        public async Task DeleteProductAsync_Should_Set_The_Product_IsDelete_Flag_To_True()
        {
            Mock<ICommonService> commonMock = new Mock<ICommonService>();
            commonMock.Setup(x => x.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("TestImageUrl");

            var repo = serviceProvider.GetService<IRepository>();

            var service = new ProductServiceAdmin(repo, commonMock.Object);

            await service.DeleteProductAsync("55861159-5a03-4e86-a6b7-bfa5104f677d");

            var product = await repo.GetByIdAsync<Product>(new Guid("55861159-5a03-4e86-a6b7-bfa5104f677d"));

            Assert.IsTrue(product.IsDeleted);
        }

        [Test]
        public void DeleteSingleProductAsync_Should_Throw_ArgumentNullException_When_The_Product_Does_Not_Exist()
        {
            Mock<ICommonService> commonMock = new Mock<ICommonService>();
            commonMock.Setup(x => x.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("TestImageUrl");

            var repo = serviceProvider.GetService<IRepository>();

            var service = new ProductServiceAdmin(repo, commonMock.Object);

            Assert.CatchAsync<ArgumentNullException>(async ()
                    => await service.DeleteSingleProductAsync("693310f0-1205-4104-b410-f3702f000fa7"),
                ProductNotFound);
        }

        [Test]
        public async Task DeleteSingleProductAsync_Should_Remove_One_From_The_Product_Quantity()
        {
            Mock<ICommonService> commonMock = new Mock<ICommonService>();
            commonMock.Setup(x => x.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("TestImageUrl");

            var repo = serviceProvider.GetService<IRepository>();

            var service = new ProductServiceAdmin(repo, commonMock.Object);

            await service.DeleteSingleProductAsync("55861159-5a03-4e86-a6b7-bfa5104f677d");

            var product = await repo.GetByIdAsync<Product>(new Guid("55861159-5a03-4e86-a6b7-bfa5104f677d"));

            Assert.That(product.Quantity, Is.EqualTo(2));
        }

        [Test]
        public async Task DeleteSingleProductAsync_Should_Set_IsDelete_To_True_If_The_Product_Quantity_Is_Equal_To_Zero()
        {
            Mock<ICommonService> commonMock = new Mock<ICommonService>();
            commonMock.Setup(x => x.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("TestImageUrl");

            var repo = serviceProvider.GetService<IRepository>();

            var service = new ProductServiceAdmin(repo, commonMock.Object);

            await service.DeleteSingleProductAsync("55861159-5a03-4e86-a6b7-bfa5104f677d");
            await service.DeleteSingleProductAsync("55861159-5a03-4e86-a6b7-bfa5104f677d");
            await service.DeleteSingleProductAsync("55861159-5a03-4e86-a6b7-bfa5104f677d");

            var product = await repo.GetByIdAsync<Product>(new Guid("55861159-5a03-4e86-a6b7-bfa5104f677d"));

            Assert.IsTrue(product.IsDeleted);
        }

        [Test]
        public void EditAsync_Should_Throw_ArgumentNullException_When_The_Product_Does_Not_Exist()
        {
            var productModel = new EditProductViewModel()
            {
                Id = new Guid("693310f0-1205-4104-b410-f3702f000fa7"),
                Title = "Test Product",
                Description = "Test",
                Price = 323,
                BrandId = new Guid("a3beeab1-6e85-47bd-b62a-bc0bd2c681ee"),
                ModelId = new Guid("57f0de91-7fdd-4342-82cd-791841651ac9"),
                TypeId = new Guid("db927cb1-89fc-4887-b6b9-8ee3512eba05"),
                Quantity = 3,
            };

            Mock<ICommonService> commonMock = new Mock<ICommonService>();
            commonMock.Setup(x => x.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("TestImageUrl");

            var repo = serviceProvider.GetService<IRepository>();

            var service = new ProductServiceAdmin(repo, commonMock.Object);

            Assert.CatchAsync<ArgumentNullException>(async ()
                    => await service.EditAsync(productModel),
                ProductNotFound);
        }

        [Test]
        public async Task EditAsync_Should_Edit_Product_Correctly()
        {
            var productModel = new EditProductViewModel()
            {
                Id = new Guid("55861159-5a03-4e86-a6b7-bfa5104f677d"),
                Title = "Test Product Edited",
                Description = "Test Edited",
                Price = 323,
                BrandId = new Guid("a3beeab1-6e85-47bd-b62a-bc0bd2c681ee"),
                ModelId = new Guid("57f0de91-7fdd-4342-82cd-791841651ac9"),
                TypeId = new Guid("db927cb1-89fc-4887-b6b9-8ee3512eba05"),
                Quantity = 3,
            };

            Mock<ICommonService> commonMock = new Mock<ICommonService>();
            commonMock.Setup(x => x.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("TestImageUrl");

            var repo = serviceProvider.GetService<IRepository>();

            var service = new ProductServiceAdmin(repo, commonMock.Object);

            await service.EditAsync(productModel);

            var editedProduct = await repo.GetByIdAsync<Product>(new Guid("55861159-5a03-4e86-a6b7-bfa5104f677d"));

            Assert.That(editedProduct.Title, Is.EqualTo("Test Product Edited"));
        }

        [Test]
        public async Task GetAllProductsAsync_Should_Return_All_Products_From_The_Database()
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

            Mock<ICommonService> commonMock = new Mock<ICommonService>();
            commonMock.Setup(x => x.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("TestImageUrl");

            var repo = serviceProvider.GetService<IRepository>();

            var service = new ProductServiceAdmin(repo, commonMock.Object);
            await service.AddNewProductAsync(productModel);

            var products = await service.GetAllProductsAsync();

            Assert.That(products.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetForEditAsync_Should_Throw_ArgumentNullException_When_Product_Is_Not_Found()
        {
            Mock<ICommonService> commonMock = new Mock<ICommonService>();
            commonMock.Setup(x => x.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("TestImageUrl");

            var repo = serviceProvider.GetService<IRepository>();

            var service = new ProductServiceAdmin(repo, commonMock.Object);

            Assert.CatchAsync<ArgumentNullException>(async ()
                    => await service.GetForEditAsync("7474ef2a-adf7-4658-80da-64bb31bc7192"),
                ProductNotFound);
        }

        [Test]
        public async Task GetForEditAsync_Should_Returns_The_Product_That_Correspond_To_The_Passed_Identifier()
        {
            Mock<ICommonService> commonMock = new Mock<ICommonService>();
            commonMock.Setup(x => x.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("TestImageUrl");

            var repo = serviceProvider.GetService<IRepository>();

            var service = new ProductServiceAdmin(repo, commonMock.Object);

            var product = await service.GetForEditAsync("55861159-5a03-4e86-a6b7-bfa5104f677d");

            Assert.IsNotNull(product);
            Assert.That(product.Title, Is.EqualTo("Test Product"));
        }

        [Test]
        public async Task GetAllDeletedProductsAsync_Should_Returns_All_Products_With_IsDeleted_Flag_To_True()
        {
            Mock<ICommonService> commonMock = new Mock<ICommonService>();
            commonMock.Setup(x => x.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("TestImageUrl");

            var repo = serviceProvider.GetService<IRepository>();

            var service = new ProductServiceAdmin(repo, commonMock.Object);

            await service.DeleteProductAsync("55861159-5a03-4e86-a6b7-bfa5104f677d");

            var products = await service.GetAllDeletedProductsAsync();

            Assert.That(products.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task ReturnDeletedProductAsync_Should_Set_IsDeleted_Flag_To_False_And_Update_Quantity()
        {
            Mock<ICommonService> commonMock = new Mock<ICommonService>();
            commonMock.Setup(x => x.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("TestImageUrl");

            var repo = serviceProvider.GetService<IRepository>();

            var service = new ProductServiceAdmin(repo, commonMock.Object);

            await service.DeleteProductAsync("55861159-5a03-4e86-a6b7-bfa5104f677d");
            await service.ReturnDeletedProductAsync("55861159-5a03-4e86-a6b7-bfa5104f677d", 23);

            var products = await service.GetAllProductsAsync();
            var product = products
                .FirstOrDefault();

            Assert.That(product.Quantity, Is.EqualTo(23));
            Assert.That(products.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task AddOthersAsync_Should_Should_Add_New_Type_To_The_Database()
        {
            Mock<ICommonService> commonMock = new Mock<ICommonService>();
            commonMock.Setup(x => x.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("TestImageUrl");

            var repo = serviceProvider.GetService<IRepository>();

            var service = new ProductServiceAdmin(repo, commonMock.Object);

            await service.AddOthersAsync("Type", "NewTypeTest");

            var types = await repo
                .AllReadonly<Type>()
                .ToListAsync();

            var newType = types
                .FirstOrDefault(x => x.Name == "NewTypeTest");

            Assert.That(types.Count(), Is.EqualTo(4));
            Assert.NotNull(newType);
        }

        [Test]
        public async Task AddOthersAsync_Should_Should_Add_New_Model_To_The_Database()
        {
            Mock<ICommonService> commonMock = new Mock<ICommonService>();
            commonMock.Setup(x => x.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("TestImageUrl");

            var repo = serviceProvider.GetService<IRepository>();

            var service = new ProductServiceAdmin(repo, commonMock.Object);

            await service.AddOthersAsync("Model", "NewModelTest");

            var models = await repo
                .AllReadonly<Model>()
                .ToListAsync();

            var newModel = models
                .FirstOrDefault(x => x.Name == "NewModelTest");

            Assert.That(models.Count(), Is.EqualTo(4));
            Assert.NotNull(newModel);
        }

        [Test]
        public async Task AddOthersAsync_Should_Should_Add_New_Brand_To_The_Database()
        {
            Mock<ICommonService> commonMock = new Mock<ICommonService>();
            commonMock.Setup(x => x.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync("TestImageUrl");

            var repo = serviceProvider.GetService<IRepository>();

            var service = new ProductServiceAdmin(repo, commonMock.Object);

            await service.AddOthersAsync("Brand", "NewBrandTest");

            var brands = await repo
                .AllReadonly<Brand>()
                .ToListAsync();

            var newBrand = brands
                .FirstOrDefault(x => x.Name == "NewBrandTest");

            Assert.That(brands.Count(), Is.EqualTo(4));
            Assert.NotNull(newBrand);
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
                BrandId = new Guid("a3beeab1-6e85-47bd-b62a-bc0bd2c681ee"),
                Description = "Test Description",
                ImageUrl = "TestUrl",
                Price = 123,
                Title = "Test Product",
                ModelId = new Guid("57f0de91-7fdd-4342-82cd-791841651ac9"),
                TypeId = new Guid("db927cb1-89fc-4887-b6b9-8ee3512eba05"),
                Quantity = 3
            };

            await repo.AddAsync(product);

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