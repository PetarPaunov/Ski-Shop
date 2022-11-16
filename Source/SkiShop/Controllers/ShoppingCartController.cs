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

            var productsCout = await shoppingCartService.CartProductsCoutAsync(userId);

            HttpContext.Session.SetInt32("ProductsCout", productsCout);

            return Redirect($"/Product/ShowProduct/{productId}");
        }

        public async Task<IActionResult> RemoveFromCart(string productId)
        {
            var userId = User.Id();

            await shoppingCartService.RemoveFromCartAsync(productId, userId);

            var productsCout = await shoppingCartService.CartProductsCoutAsync(userId);

            HttpContext.Session.SetInt32("ProductsCout", productsCout);

            return RedirectToAction(nameof(Index));
        }
    }
}
