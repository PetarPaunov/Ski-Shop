using SkiShop.Core.Models.ShoppingCartViewModels;

namespace SkiShop.Core.Contracts.ShoppingCart
{
    public interface IShoppingCartService
    {
        Task AddProductInShoppingCartAsync(string productId, string userId, int quantity);
        Task<IEnumerable<ShoppingCartProductViewModel>> AllShoppingCartProductsAsync(string userId);
        Task RemoveFromCart(string productId, string userId);
    }
}
