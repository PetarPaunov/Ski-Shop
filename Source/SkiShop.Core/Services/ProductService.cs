using Microsoft.EntityFrameworkCore;
using SkiShop.Core.Contracts;
using SkiShop.Core.Models.ProductViewModels;
using SkiShop.Data.Common;
using SkiShop.Data.Models.ProductCommon;

namespace SkiShop.Core.Services
{
	public class ProductService : IProductService
	{
        private readonly IRepository repository;

        public ProductService(IRepository _repository)
        {
            repository = _repository;
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

        public async Task<IEnumerable<AllProductsAdminViewModel>> GetAllProductsAsync()
		{
            var products = await repository.All<Product>()
                .Where(x => x.IsDeleted != true)
                .Include(x => x.Model)
                .Include(x => x.Type)
                .Include(x => x.Brand)
                .Select(x => new AllProductsAdminViewModel()
                {
                    Id = x.Id.ToString(),
                    Title = x.Title,
                    Desctriptnio = x.Description,
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
	}
}
