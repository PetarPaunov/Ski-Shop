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
        private const int MAX_PRODUCTS_PER_PAGE = 12;

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

            await repository.AddAsync(productComment);
            await repository.AddAsync(userComment);

            await repository.SaveChangesAsync();
        }

        public async Task<ProductPagingViewModel> GetAllProductsAsync(int currentPage, string? type = null, string? searchTerm = null)
        {
            var products =  repository.AllReadonly<Product>()
                .Include(x => x.Type)
                .Where(x => x.IsDeleted == false);

            var model = new ProductPagingViewModel();

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
                .Skip((currentPage - 1) * MAX_PRODUCTS_PER_PAGE)
                .Select(x => new AllProductsViewModel()
                {
                    Id = x.Id.ToString(),
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    Price = x.Price.ToString()
                })
                .Take(MAX_PRODUCTS_PER_PAGE)
                .ToListAsync();

            var allProducts = await repository.AllReadonly<Product>()
                .Where(x => x.IsDeleted == false).ToListAsync();

            var pageCount = (decimal)allProducts.Count() /Convert.ToDecimal(MAX_PRODUCTS_PER_PAGE);

            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;

            return model;
        }

        public async Task<ProductPagingViewModel> GetAllProductsByTypeAsync(string type, int currentPage)
        {
            var typeExist = await repository.AllReadonly<Type>().AnyAsync(x => x.Name == type);

            if (!typeExist)
            {
                throw new ArgumentException("The Type does not exsit in the database");
            }

            var model = new ProductPagingViewModel();

            model.Products = await repository.AllReadonly<Product>()
                .Include(x => x.Type)
                .Where(x => x.IsDeleted == false && x.Type.Name == type)
                .OrderBy(x => x.CreateOn)
                .Skip((currentPage - 1) * MAX_PRODUCTS_PER_PAGE)
                .Select(x => new AllProductsViewModel()
                {
                    Id = x.Id.ToString(),
                    Title = x.Title,
                    Price = x.Price.ToString(),
                    ImageUrl = x.ImageUrl
                })
                .Take(MAX_PRODUCTS_PER_PAGE)
                .ToListAsync();

            var allProducts = await repository.AllReadonly<Product>()
                .Include(x => x.Type)
                .Where(x => x.IsDeleted == false &&x.Type.Name == type).ToListAsync();

            var pageCount = (decimal)allProducts.Count() / Convert.ToDecimal(MAX_PRODUCTS_PER_PAGE);

            model.PageCount = (int)Math.Ceiling(pageCount);
            model.CurrentPageIndex = currentPage;

            return model;
        }

        public async Task<IEnumerable<AllProductsViewModel>> GetFirstSixProductsAsync()
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