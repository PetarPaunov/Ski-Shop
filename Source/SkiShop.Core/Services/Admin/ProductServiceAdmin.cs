namespace SkiShop.Core.Services
{
    using SkiShop.Data.Common;
    using SkiShop.Core.Contracts;
    using SkiShop.Core.Contracts.Common;
    using Microsoft.EntityFrameworkCore;
    using SkiShop.Core.Models.TypeModels;
    using SkiShop.Data.Models.Product;
    using SkiShop.Core.Models.BrandModels;
    using SkiShop.Core.Models.ModelViewModels;
    using SkiShop.Core.Models.ProductViewModels;
    public class ProductServiceAdmin : IProductServiceAdmin
	{
        private readonly IRepository repository;
        private readonly ICommonService commonService;

        public ProductServiceAdmin(IRepository _repository, ICommonService _commonService)
        {
            repository = _repository;
            commonService = _commonService;
        }

        public async Task AddNewProductAsync(AddProductViewModel model)
        {
            var imageUrl = await commonService.UploadeImage(model.FrontImage);

            var product = new Product()
            {
                Title = model.Title,
                Description = model.Description,
                BrandId = model.BrandId,
                ModelId = model.ModelId,
                Price = model.Price,
                Quantity = model.Quantity,
                ImageUrl = imageUrl,
                TypeId = model.TypeId,
                CreateOn = DateTime.UtcNow
            };

            await repository.AddAsync(product);
            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<BrandViewModel>> GetAllBrandsAsync()
        {
            return await repository.All<Brand>()
                .Select(x => new BrandViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ModelViewModel>> GetAllModelsAsync()
        {
            return await repository.All<Model>()
                .Select(x => new ModelViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<TypeViewModel>> GetAllTypesAsync()
        {
            return await repository.All<Data.Models.Product.Type>()
                .Select(x => new TypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }

        public async Task DeleteProduct(string id)
        {
            var productGuid = Guid.Parse(id);

            var product = await repository.GetByIdAsync<Product>(productGuid);

            product.IsDeleted = true;
            product.Quantity = 0;

            await repository.SaveChangesAsync();
        }

        public async Task DeleteSingleProductAsync(string id)
        {
            var productGuid = Guid.Parse(id);

            var product = await repository.GetByIdAsync<Product>(productGuid);

            product.Quantity--;

            if (product.Quantity == 0)
            {
                product.IsDeleted = true;
            }

            await repository.SaveChangesAsync();
        }

        public async Task EditAsync(EditProductViewModel model)
        {
            var product = await repository.GetByIdAsync<Product>(model.Id);

            if (model.FrontImage != null)
            {
                var imageUrl = await commonService.UploadeImage(model.FrontImage);

                product.ImageUrl = imageUrl;
            }

            product.Title = model.Title;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Quantity = model.Quantity;
            product.BrandId = model.BrandId;
            product.TypeId = model.TypeId;
            product.ModelId = model.ModelId;

            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllProductsViewModel>> GetAllProductsAsync()
		{
            var products = await repository.All<Product>()
                .Where(x => x.IsDeleted != true)
                .OrderByDescending(x => x.CreateOn)
                .Include(x => x.Model)
                .Include(x => x.Type)
                .Include(x => x.Brand)
                .Select(x => new AllProductsViewModel()
                {
                    Id = x.Id.ToString(),
                    Title = x.Title,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    Brand = x.Brand.Name,
                    Model = x.Model.Name,
                    Type = x.Type.Name,
                    Price = x.Price.ToString(),
                    Quantity = x.Quantity
                })
                .ToListAsync();

            return products;
		}

        public async Task<EditProductViewModel> GetForEditAsync(string id)
        {
            var productGuid = Guid.Parse(id);

            var product = await repository.GetByIdAsync<Product>(productGuid);

            return new EditProductViewModel()
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                ModelId = product.ModelId,
                BrandId = product.BrandId,
                TypeId = product.TypeId,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Quantity = product.Quantity,
            };
        }

        public async Task<IEnumerable<AllProductsViewModel>> GetAllDeletedProductsAsync()
        {
            var products = await repository.All<Product>()
               .Where(x => x.IsDeleted == true)
               .OrderByDescending(x => x.CreateOn)
               .Include(x => x.Model)
               .Include(x => x.Type)
               .Include(x => x.Brand)
               .Select(x => new AllProductsViewModel()
               {
                   Id = x.Id.ToString(),
                   Title = x.Title,
                   Description = x.Description,
                   ImageUrl = x.ImageUrl,
                   Brand = x.Brand.Name,
                   Model = x.Model.Name,
                   Type = x.Type.Name,
                   Price = x.Price.ToString(),
                   Quantity = x.Quantity
               })
               .ToListAsync();

            return products;
        }

        public async Task ReturnDeletedProduct(string productId, int quantity)
        {
            var productGuid = Guid.Parse(productId);

            var product = await repository.GetByIdAsync<Product>(productGuid);

            product.IsDeleted = false;
            product.Quantity = quantity;

            await repository.SaveChangesAsync();
        }

        public async Task AddOthers(string keyWord, string name)
        {
            if (keyWord == nameof(Type))
            {
                await repository.AddAsync(new Type()
                {
                    Name = name,
                });
            }
            else if (keyWord == nameof(Brand))
            {
                await repository.AddAsync(new Brand()
                {
                    Name = name,
                });
            }
            else if (keyWord == nameof(Model))
            {
                await repository.AddAsync(new Model()
                {
                    Name = name,
                });
            }

            await repository.SaveChangesAsync();
        }
    }
}