namespace SkiShop.Controllers
{
    using SkiShop.Extension;
    using SkiShop.Core.Constants;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using SkiShop.Core.Contracts.ShoppingCart;

    using static SkiShop.Core.Constants.ToastrMessagesConstants;

    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly ILogger<ShoppingCartController> logger;

        public ShoppingCartController(IShoppingCartService _shoppingCartService, 
                                      ILogger<ShoppingCartController> _logger)
        {
            shoppingCartService = _shoppingCartService;
            logger = _logger;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.Id();

            var model = await shoppingCartService.AllShoppingCartProductsAsync(userId);

            return View(model);
        }

        public async Task<IActionResult> AddToCart(int quantity, string productId)
        {
            try
            {
                var userId = User.Id();

                await shoppingCartService.AddProductInShoppingCartAsync(productId, userId, quantity);
                var productsCount = await shoppingCartService.CartProductsCoutAsync(userId);

                HttpContext.Session.SetInt32("ProductsCount", productsCount);
                return Redirect($"/Product/ShowProduct/{productId}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }

        public async Task<IActionResult> RemoveFromCart(string productId)
        {
            try
            {
                var userId = User.Id();

                await shoppingCartService.RemoveFromCartAsync(productId, userId);
                var productsCount = await shoppingCartService.CartProductsCoutAsync(userId);

                HttpContext.Session.SetInt32("ProductsCount", productsCount);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }

        public async Task<IActionResult> PlaceOrder()
        {
            try
            {
                var userId = User.Id();

                await shoppingCartService.PlaceUserOrderAsync(userId);
                var productsCount = await shoppingCartService.CartProductsCoutAsync(userId);

                HttpContext.Session.SetInt32("ProductsCount", productsCount);
                TempData[MessageConstant.SuccessMessage] = OrderReceived;

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }
    }
}