using Microsoft.EntityFrameworkCore;
using SkiShop.Core.Contracts;
using SkiShop.Core.Contracts.Common;
using SkiShop.Core.Models.ProductViewModels;
using SkiShop.Data.Common;
using SkiShop.Data.Models.ProductCommon;

namespace SkiShop.Core.Services
{
	public class ProductService : IProductService
	{
        private readonly IRepository repository;
        private readonly ICommonService commonService;

        public ProductService(IRepository _repository, ICommonService _commonService)
        {
            repository = _repository;
            commonService = _commonService;
        }

        public async Task DeleteProduct(string id)
        {
            var productGuid = Guid.Parse(id);

            var product = await repository.GetByIdAsync<Product>(productGuid);

            product.IsDeleted = true;

            await repository.SaveChangesAsync();
        }

        public async Task DeleteSingleProductAsync(string id)
        {
            var productGuid = Guid.Parse(id);

            var product = await repository.GetByIdAsync<Product>(productGuid);

            product.Quantity--;

            await repository.SaveChangesAsync();
        }

        public async Task EditAsync(EditProductViewModel model)
        {
            var product = await repository.GetByIdAsync<Product>(model.Id);

            var imageUrl = commonService.UploadedFile(model.FrontImage);

            product.Title = model.Title;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Quantity = model.Quantity;
            product.BrandId = model.BrandId;
            product.TypeId = model.TypeId;
            product.ModelId = model.ModelId;
            product.ImageUrl = imageUrl;

            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductsViewModel>> GetAllProductsAsync()
		{
            var products = await repository.All<Product>()
                .Where(x => x.IsDeleted != true)
                .Include(x => x.Model)
                .Include(x => x.Type)
                .Include(x => x.Brand)
                .Select(x => new ProductsViewModel()
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
    }
}