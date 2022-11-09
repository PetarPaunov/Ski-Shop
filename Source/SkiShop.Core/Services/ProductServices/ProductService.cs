﻿namespace SkiShop.Core.Services.ProductServices
{
    using Microsoft.EntityFrameworkCore;
    using SkiShop.Core.Contracts.ProductContracts;
    using SkiShop.Core.Models.CommentViewModels;
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

        public async Task AddNewComment(string comment, string productId)
        {
            var guidProductId = new Guid(productId);

            var newComment = new Comment()
            {
                Description = comment
            };

            var product = await repository.GetByIdAsync<Product>(guidProductId);

            var productComment = new ProductComment()
            {
                Comment = newComment,
                Product = product,
            };

            await repository.AddAsync<ProductComment>(productComment);

            await repository.SaveChangesAsync();
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
                .Select(x => new CommentViewModel()
                {
                    Id = x.Comment.Id,
                    Description = x.Comment.Description
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
