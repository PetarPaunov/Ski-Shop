namespace SkiShop.Core.Services
{
    using SkiShop.Data.Common;
    using SkiShop.Core.Contracts;
    using SkiShop.Data.Models.Product;
    using SkiShop.Core.Contracts.Common;
    using Microsoft.EntityFrameworkCore;
    using SkiShop.Core.Models.TypeModels;
    using SkiShop.Core.Models.BrandModels;
    using SkiShop.Core.Models.ModelViewModels;
    using SkiShop.Core.Models.ProductViewModels;

    using static SkiShop.Core.Constants.ExeptionMessagesConstants;

    /// <summary>
    /// Administrator services for the product
    /// </summary>
    public class ProductServiceAdmin : IProductServiceAdmin
	{
        private readonly IRepository repository;
        private readonly ICommonService commonService;

        public ProductServiceAdmin(IRepository _repository, 
                                   ICommonService _commonService)
        {
            repository = _repository;
            commonService = _commonService;
        }

        /// <summary>
        /// Add a new product to the database
        /// </summary>
        /// <param name="model">Object with required product data</param>
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

        /// <summary>
        /// Gets all product banrds from the database
        /// </summary>
        /// <returns>A collection of brands mapped to a view model</returns>
        public async Task<IEnumerable<BrandViewModel>> GetAllBrandsAsync()
        {
           var brands = await repository.All<Brand>()
                .Select(x => new BrandViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();

            return brands;
        }

        /// <summary>
        /// Gets all product models from the database
        /// </summary>
        /// <returns>A collection of models mapped to a view model</returns>
        public async Task<IEnumerable<ModelViewModel>> GetAllModelsAsync()
        {
            var models = await repository.All<Model>()
                .Select(x => new ModelViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();

            return models;
        }

        /// <summary>
        /// Gets all product types from the database
        /// </summary>
        /// <returns>A collection of types mapped to a view model</returns>
        public async Task<IEnumerable<TypeViewModel>> GetAllTypesAsync()
        {
            var types = await repository.All<Type>()
                .Select(x => new TypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();

            return types;
        }

        /// <summary>
        /// Sets the IsDeleted flag to true (product is deleted)
        /// </summary>
        /// <param name="id">Identifier of the product</param>
        public async Task DeleteProductAsync(string id)
        {
            var productGuid = Guid.Parse(id);

            var product = await repository.GetByIdAsync<Product>(productGuid);

            if (product == null)
            {
                throw new ArgumentNullException(ProductNotFound);
            }

            product.IsDeleted = true;
            product.Quantity = 0;

            await repository.SaveChangesAsync();
        }

        /// <summary>
        /// Remove one from the product quantity
        /// </summary>
        /// <param name="id">Identifire of the product</param>
        public async Task DeleteSingleProductAsync(string id)
        {
            var productGuid = Guid.Parse(id);

            var product = await repository.GetByIdAsync<Product>(productGuid);

            if (product == null)
            {
                throw new ArgumentNullException(ProductNotFound);
            }

            product.Quantity--;

            if (product.Quantity == 0)
            {
                product.IsDeleted = true;
            }

            await repository.SaveChangesAsync();
        }

        /// <summary>
        /// Edit existing product
        /// </summary>
        /// <param name="model">Contains the product data for edit</param>
        public async Task EditAsync(EditProductViewModel model)
        {
            var product = await repository.GetByIdAsync<Product>(model.Id);

            if (product == null)
            {
                throw new ArgumentNullException(ProductNotFound);
            }

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

        /// <summary>
        /// Gets all products from the database
        /// </summary>
        /// <returns>A collection of products mapped to a view model</returns>
        public async Task<IEnumerable<AllProductsAdminViewModel>> GetAllProductsAsync()
		{
            var products = await repository.All<Product>()
                .Where(x => x.IsDeleted != true)
                .OrderByDescending(x => x.CreateOn)
                .Include(x => x.Model)
                .Include(x => x.Type)
                .Include(x => x.Brand)
                .Select(x => new AllProductsAdminViewModel()
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

        /// <summary>
        /// Get existing product from database for editing
        /// </summary>
        /// <param name="id">Identifier of the product</param>
        /// <returns>A product mapped to a view model</returns>
        public async Task<EditProductViewModel> GetForEditAsync(string id)
        {
            var productGuid = Guid.Parse(id);

            var product = await repository.GetByIdAsync<Product>(productGuid);

            if (product == null)
            {
                throw new ArgumentNullException(ProductNotFound);
            }

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

        /// <summary>
        /// Gets all products with (IsDeleted flag = ture) from the database
        /// </summary>
        /// <returns>A collection of products mapped to a view model</returns>
        public async Task<IEnumerable<AllProductsAdminViewModel>> GetAllDeletedProductsAsync()
        {
            var products = await repository.All<Product>()
               .Where(x => x.IsDeleted == true)
               .OrderByDescending(x => x.CreateOn)
               .Include(x => x.Model)
               .Include(x => x.Type)
               .Include(x => x.Brand)
               .Select(x => new AllProductsAdminViewModel()
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

        /// <summary>
        /// Sets IsDeleted flag to false and add a new quantity for the product
        /// </summary>
        /// <param name="productId">The id of the deleted product</param>
        /// <param name="quantity">Quantity for the product</param>
        public async Task ReturnDeletedProductAsync(string productId, int quantity)
        {
            var productGuid = Guid.Parse(productId);

            var product = await repository.GetByIdAsync<Product>(productGuid);

            if (product == null)
            {
                throw new ArgumentNullException(ProductNotFound);
            }

            product.IsDeleted = false;
            product.Quantity = quantity;

            await repository.SaveChangesAsync();
        }

        /// <summary>
        /// Add a new Type, Brand or Model to database
        /// </summary>
        /// <param name="keyWord">Can be Type, Brand, Model</param>
        /// <param name="name">Name of the entity</param>
        public async Task AddOthersAsync(string keyWord, string name)
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