namespace SkiShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SkiShop.Core.Constants;
    using SkiShop.Core.Contracts.ProductContracts;
    using SkiShop.Models;
    using System.Diagnostics;

    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> logger;
        private readonly IProductService productService;
        public HomeController(ILogger<HomeController> _logger, IProductService _productService)
        {
            this.logger = _logger;
            productService = _productService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = await productService.GetFirstSixProductsAsync();

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ShowProduct(string id)
        {
            var model = await productService.GetProductByIdAsync(id);

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}