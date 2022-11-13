namespace SkiShop.Core.Services.ShoppingCart
{
    using Microsoft.EntityFrameworkCore;
    using SkiShop.Core.Contracts.ShoppingCart;
    using SkiShop.Core.Models.ShoppingCartViewModels;
    using SkiShop.Data.Common;
    using SkiShop.Data.Models.Account;
    using SkiShop.Data.Models.Product;
    using SkiShop.Data.Models.ShoppingCart;
    using System.Collections.Generic;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository repository;

        public ShoppingCartService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task AddProductInShoppingCartAsync(string productId, string userId, int quantity)
        {
            var productGuidId = new Guid(productId);

            var product = await repository.GetByIdAsync<Product>(productGuidId);
            var user = await repository.GetByIdAsync<ApplicationUser>(userId);

            if (product == null)
            {
                return; //TODO..
            }

            if (user == null)
            {
                return; //TODO.. 
            }

            var shoppingCartProduct = await GetShoppingCartProduct(productGuidId, user.ShoppingCartId);

            if (shoppingCartProduct != null)
            {
                return; //TODO..
            }

            shoppingCartProduct = new ShoppingCartProduct()
            {
                Product = product,
                Quantity = quantity,
                ShoppingCartId = user.ShoppingCartId
            };


            await repository.AddAsync(shoppingCartProduct);
            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ShoppingCartProductViewModel>> AllShoppingCartProductsAsync(string userId)
        {
            var user = await repository.GetByIdAsync<ApplicationUser>(userId);

            var allProducts = await repository.All<ShoppingCartProduct>()
                .Where(x => x.ShoppingCartId == user.ShoppingCartId)
                .Select(x => new ShoppingCartProductViewModel()
                {
                    Id = x.Product.Id,
                    Name = x.Product.Title,
                    ImageUrl = x.Product.ImageUrl,
                    Price = x.Product.Price,
                    Quantity = x.Quantity,
                    TotalPrice = (x.Product.Price * x.Quantity).ToString("F2")
                })
                .ToListAsync();

            return allProducts;
        }

        private async Task<ShoppingCartProduct> GetShoppingCartProduct(Guid productId, Guid shoppingCartId)
        {
            return await repository.All<ShoppingCartProduct>()
                .FirstOrDefaultAsync(x => x.ShoppingCartId == shoppingCartId && x.ProductId == productId);
        }
    }
}
