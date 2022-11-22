namespace SkiShop.Controllers
{
    using SkiShop.Models;
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using SkiShop.Core.Contracts.ProductContracts;

    public class HomeController : BaseController
    {
        private readonly IProductService productService;
        public HomeController(IProductService _productService)
        {
            productService = _productService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = await productService.GetFirstEightProductsAsync();

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Error404()
        {
            return View();
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