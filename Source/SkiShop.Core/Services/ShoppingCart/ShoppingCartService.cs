namespace SkiShop.Core.Services.ShoppingCart
{
    using SkiShop.Data.Common;
    using System.Collections.Generic;
    using SkiShop.Data.Models.Product;
    using SkiShop.Data.Models.Account;
    using Microsoft.EntityFrameworkCore;
    using SkiShop.Data.Models.ShoppingCart;
    using SkiShop.Core.Contracts.ShoppingCart;
    using SkiShop.Core.Models.ShoppingCartViewModels;

    using static SkiShop.Core.Constants.ExeptionMessagesConstants;

    /// <summary>
    /// Services for managing shopping cart
    /// </summary>
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository repository;

        public ShoppingCartService(IRepository _repository)
        {
            repository = _repository;
        }

        /// <summary>
        /// Adds a product to the user's shopping cart
        /// </summary>
        /// <param name="productId">Identifier of the product</param>
        /// <param name="userId">Identifier of the user</param>
        /// <param name="quantity">Quantity of the product</param>
        public async Task AddProductInShoppingCartAsync(string productId, string userId, int quantity)
        {
            var productGuidId = new Guid(productId);

            var product = await repository.GetByIdAsync<Product>(productGuidId);
            var user = await repository.GetByIdAsync<ApplicationUser>(userId);

            if (product == null)
            {
                throw new ArgumentNullException(ProductNotFound);
            }

            if (user == null)
            {
                throw new ArgumentNullException(UserNotFound);
            }

            var shoppingCartProduct = await GetShoppingCartProduct(productGuidId, user.ShoppingCartId);

            if (shoppingCartProduct != null)
            {
                throw new ArgumentNullException(ShoppingCartNotFound);
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

        /// <summary>
        /// Gets all products in the user's shopping cart
        /// </summary>
        /// <param name="userId">Identifier of the user</param>
        /// <returns>A collection of products in the cart</returns>
        public async Task<IEnumerable<ShoppingCartProductViewModel>> AllShoppingCartProductsAsync
            (string userId)
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

        /// <summary>
        /// Gets the count of all products in the cart
        /// </summary>
        /// <param name="userId">Identifier of the user</param>
        /// <returns>Count of all products</returns>
        public async Task<int> CartProductsCoutAsync(string userId)
        {
            var user = await repository.GetByIdAsync<ApplicationUser>(userId);

            if (user == null)
            {
                throw new ArgumentNullException(UserNotFound);
            }

            var allProducts = await repository.AllReadonly<ShoppingCartProduct>()
                .Where(x => x.ShoppingCartId == user.ShoppingCartId).ToListAsync();

            return allProducts.Count();
        }

        /// <summary>
        /// Creates a user order
        /// </summary>
        /// <param name="userId">Identifier of the user</param>
        public async Task PlaceUserOrderAsync(string userId)
        {
            var user = await repository.GetByIdAsync<ApplicationUser>(userId);

            if (user == null)
            {
                throw new ArgumentNullException(UserNotFound);
            }

            var allProducts = await repository.All<ShoppingCartProduct>()
                .Where(x => x.ShoppingCartId == user.ShoppingCartId)
                .ToListAsync();

            foreach (var product in allProducts)
            {
                var order = new Order()
                {
                    ProductId = product.ProductId,
                    ApplicationUserId = user.Id,
                    Quantity = product.Quantity,
                };

                await repository.AddAsync(order);
                await RemoveFromCartAsync(product.ProductId.ToString(), user.Id);
            }

            await repository.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a product from the user's shopping cart
        /// </summary>
        /// <param name="productId">Identifier of the product</param>
        /// <param name="userId">Identifier of the user</param>
        public async Task RemoveFromCartAsync(string productId, string userId)
        {
            var productGuidId = new Guid(productId);

            var user = await repository.GetByIdAsync<ApplicationUser>(userId);

            if (user == null)
            {
                throw new ArgumentNullException(UserNotFound);
            }

            var shoppingCart = await GetShoppingCartProduct(productGuidId, user.ShoppingCartId);

            await repository.DeleteAsync<ShoppingCartProduct>(shoppingCart.Id);
            await repository.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a shopping cart of a user
        /// </summary>
        /// <param name="productId">Identifier of the product</param>
        /// <param name="shoppingCartId">Identifier of the user's shopping cart</param>
        /// <returns></returns>
        private async Task<ShoppingCartProduct> GetShoppingCartProduct(Guid productId, Guid shoppingCartId)
        {
            return await repository.All<ShoppingCartProduct>()
                .FirstAsync(x => x.ShoppingCartId == shoppingCartId && x.ProductId == productId);
        }
    }
}