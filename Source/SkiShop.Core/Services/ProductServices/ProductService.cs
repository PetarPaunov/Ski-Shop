namespace SkiShop.Core.Services.ProductServices
{
    using Microsoft.EntityFrameworkCore;
    using SkiShop.Core.Contracts.ProductContracts;
    using SkiShop.Core.Models.CommentViewModels;
    using SkiShop.Core.Models.ProductViewModels;
    using SkiShop.Data.Common;
    using SkiShop.Data.Models.Account;
    using SkiShop.Data.Models.Product;

    public class ProductService : IProductService
    {
        private readonly IRepository repository;

        public ProductService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task AddNewComment(string comment, string productId, string userId)
        {
            var guidProductId = new Guid(productId);

            var newComment = new Comment()
            {
                Description = comment,
                CreatedOn = DateTime.UtcNow
            };

            var product = await repository.GetByIdAsync<Product>(guidProductId);
            var user = await repository.GetByIdAsync<ApplicationUser>(userId);

            var productComment = new ProductComment()
            {
                Comment = newComment,
                Product = product,
                UserName = user.UserName
            };

            var userComment = new UserComment()
            {
                Comment = newComment,
                ApplicationUser = user
            };

            await repository.AddAsync<ProductComment>(productComment);
            await repository.AddAsync<UserComment>(userComment);

            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<HomeProductViewModel>> GetFirstSixProductsAsync()
        {
            var products = await repository.All<Product>()
                .Where(x => x.IsDeleted != true)
                .OrderByDescending(x => x.CreateOn)
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
                .Take(8)
                .ToListAsync();

            return products;
        }

        public async Task<ProductViewModel> GetProductByIdAsync(string productId)
        {
            var guid = new Guid(productId);

            var returendProduct = await repository.All<Product>()
                .Include(x => x.Model)
                .Include(x => x.Type)
                .Include(x => x.Brand)
                .FirstOrDefaultAsync(x => x.Id == guid);

            var comments = await repository.All<ProductComment>()
                .Where(x => x.ProductId == returendProduct.Id)
                .OrderByDescending(x => x.Comment.CreatedOn)
                .Select(x => new CommentViewModel()
                {
                    Id = x.Comment.Id,
                    Description = x.Comment.Description,
                    User = x.UserName,
                    CreateOn = x.Comment.CreatedOn.Date.ToString()
                })
                .ToListAsync();

            var product = new ProductViewModel()
            {
                Id = returendProduct.Id.ToString(),
                Title = returendProduct.Title,
                Description = returendProduct.Description,
                Brand = returendProduct.Brand.Name,
                Model = returendProduct.Model.Name,
                Type = returendProduct.Type.Name,
                Price = returendProduct.Price.ToString(),
                Quantity = returendProduct.Quantity,
                ImageUrl = returendProduct.ImageUrl,
                Comments = comments
            };

            return product;
        }
    }
}