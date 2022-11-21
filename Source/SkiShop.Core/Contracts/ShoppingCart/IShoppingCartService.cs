namespace SkiShop.Core.Contracts.ShoppingCart
{
    using SkiShop.Core.Models.ShoppingCartViewModels;

    /// <summary>
    /// Services for managing shopping cart
    /// </summary>
    public interface IShoppingCartService
    {
        /// <summary>
        /// Adds a product to the user's shopping cart
        /// </summary>
        /// <param name="productId">Identifier of the product</param>
        /// <param name="userId">Identifier of the user</param>
        /// <param name="quantity">Quantity of the product</param>
        Task AddProductInShoppingCartAsync(string productId, string userId, int quantity);

        /// <summary>
        /// Gets all products in the user's shopping cart
        /// </summary>
        /// <param name="userId">Identifier of the user</param>
        /// <returns>A collection of products in the cart</returns>
        Task<IEnumerable<ShoppingCartProductViewModel>> AllShoppingCartProductsAsync(string userId);

        /// <summary>
        /// Gets the count of all products in the cart
        /// </summary>
        /// <param name="userId">Identifier of the user</param>
        /// <returns>Count of all products</returns>
        Task<int> CartProductsCoutAsync(string userId);

        /// <summary>
        /// Removes a product from the user's shopping cart
        /// </summary>
        /// <param name="productId">Identifier of the product</param>
        /// <param name="userId">Identifier of the user</param>
        Task RemoveFromCartAsync(string productId, string userId);

        /// <summary>
        /// Creates a user order
        /// </summary>
        /// <param name="userId">Identifier of the user</param>
        Task PlaceUserOrderAsync(string userId);
    }
}