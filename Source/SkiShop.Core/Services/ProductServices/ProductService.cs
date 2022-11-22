namespace SkiShop.Core.Services.ProductServices
{
    using SkiShop.Data.Common;
    using SkiShop.Data.Models.Account;
    using SkiShop.Data.Models.Product;
    using Microsoft.EntityFrameworkCore;
    using SkiShop.Core.Models.CommentViewModels;
    using SkiShop.Core.Models.ProductViewModels;
    using SkiShop.Core.Contracts.ProductContracts;

    using static SkiShop.Core.Constants.ExeptionMessagesConstants;

    public class ProductService : IProductService
    {
        private readonly IRepository repository;

        public ProductService(IRepository _repository)
        {
            repository = _repository;
        }

        /// <summary>
        /// Add a comment to product
        /// </summary>
        /// <param name="comment">Comment description</param>
        /// <param name="productId">Identifier of the product</param>
        /// <param name="userId">Identifier of the user</param>
        public async Task AddNewComment(string comment, string productId, string userId)
        {
            var guidProductId = new Guid(productId);

            var newComment = new Comment()
            {
                Description = comment,
                CreatedOn = DateTime.UtcNow
            };

            var product = await repository.GetByIdAsync<Product>(guidProductId);

            if (product == null)
            {
                throw new ArgumentNullException(ProductNotFound);
            }
            var user = await repository.GetByIdAsync<ApplicationUser>(userId);

            if (user == null)
            {
                throw new ArgumentNullException(UserNotFound);
            }

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

            await repository.AddAsync(productComment);
            await repository.AddAsync(userComment);

            await repository.SaveChangesAsync();
        }

        /// <summary>
        /// Gets all products from the database base on conditions
        /// </summary>
        /// <param name="currentPage">Current page of the view</param>
        /// <param name="productsPerPage">Maximum products per page</param>
        /// <param name="type">Type of the product</param>
        /// <param name="searchTerm">Search term</param>
        /// <returns>View model holding the data get depending on conditions</returns>
        public async Task<ProductQueryViewModel> GetAllProductsAsync
            (int currentPage, int productPerPage = 1, string? type = null, string? searchTerm = null)
        {
            var products =  repository.AllReadonly<Product>()
                .Include(x => x.Type)
                .Where(x => x.IsDeleted == false);

            var model = new ProductQueryViewModel();

            if (string.IsNullOrEmpty(type) == false)
            {
                products = products
                    .Where(x => x.Type.Name == type);
            }

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                searchTerm = $"%{searchTerm.ToLower()}%";

                products = products
                    .Where(h => EF.Functions.Like(h.Title.ToLower(), searchTerm) ||
                        EF.Functions.Like(h.Type.Name.ToLower(), searchTerm) ||
                        EF.Functions.Like(h.Description.ToLower(), searchTerm));
            }

            model.Products = await products
                .OrderBy(x => x.CreateOn)
                .Skip((currentPage - 1) * productPerPage)
                .Select(x => new AllProductsViewModel()
                {
                    Id = x.Id.ToString(),
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    Price = x.Price.ToString()
                })
                .Take(productPerPage)
                .ToListAsync();

            model.TotalProductsCount = await products.CountAsync();

            return model;
        }

        /// <summary>
        /// Gets first six products in the databse
        /// </summary>
        /// <returns>A collection of products mapped to a view model</returns>
        public async Task<IEnumerable<AllProductsViewModel>> GetFirstEightProductsAsync()
        {
            var products = await repository.All<Product>()
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.CreateOn)
                .Include(x => x.Model)
                .Include(x => x.Type)
                .Include(x => x.Brand)
                .Select(x => new AllProductsViewModel()
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

        /// <summary>
        /// Gets a product from the database
        /// </summary>
        /// <param name="productId">Identifier of the product</param>
        /// <returns>A single product mapped to a view model</returns>
        public async Task<ProductViewModel> GetProductByIdAsync(string productId)
        {
            var guid = new Guid(productId);

            var returendProduct = await repository.All<Product>()
                .Include(x => x.Model)
                .Include(x => x.Type)
                .Include(x => x.Brand)
                .FirstOrDefaultAsync(x => x.Id == guid);

            if (returendProduct == null)
            {
                throw new ArgumentNullException(ProductNotFound);
            }

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