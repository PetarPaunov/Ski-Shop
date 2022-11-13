namespace SkiShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SkiShop.Core.Contracts.ShoppingCart;
    using SkiShop.Extension;

    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IShoppingCartService _shoppingCartService)
        {
            shoppingCartService = _shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.Id();

            var model = await shoppingCartService.AllShoppingCartProductsAsync(userId);

            return View(model);
        }

        public async Task<IActionResult> AddToCart(int quantity, string productId)
        {
            var userId = User.Id();

            await shoppingCartService.AddProductInShoppingCartAsync(productId, userId, quantity);

            return Redirect($"/Home/ShowProduct/{productId}");
        }
    }
}
