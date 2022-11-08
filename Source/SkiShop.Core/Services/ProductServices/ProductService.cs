namespace SkiShop.Core.Services.ProductServices
{
    using Microsoft.EntityFrameworkCore;
    using SkiShop.Core.Contracts.ProductContracts;
    using SkiShop.Core.Models.ProductViewModels;
    using SkiShop.Data.Common;
    using SkiShop.Data.Models.Product;

    public class ProductService : IProductService
    {
        private readonly IRepository repository;

        public ProductService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<HomeProductViewModel>> GetFirstSixProductsAsync()
        {
            var products = await repository.All<Product>()
                .Where(x => x.IsDeleted != true)
                .Include(x => x.Model)
                .Include(x => x.Type)
                .Include(x => x.Brand)
                .Select(x => new HomeProductViewModel()
                {
                    Id = x.Id.ToString(),
                    Title = x.Title,
                    Price = x.Price.ToString(),
                    ImageUrl = x.ImageUrl
                })
                .Take(6)
                .ToListAsync();

            return products;
        }
    }
}
