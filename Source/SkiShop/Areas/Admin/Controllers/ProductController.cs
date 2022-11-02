namespace SkiShop.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SkiShop.Core.Contracts;
    using SkiShop.Core.Models.ProductViewModels;

    public class ProductController : BaseController
    {
        private readonly IProductServiceAdmin productService;

        public ProductController(IProductServiceAdmin _productService)
        {
            productService = _productService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await productService.GetAllProductsAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var types = await productService.GetAllTypesAsync();
            var models = await productService.GetAllModelsAsync();
            var brands = await productService.GetAllBrandsAsync();

            ViewBag.Types = types;
            ViewBag.Models = models;
            ViewBag.Brands = brands;

            var model = new AddProductViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await productService.AddNewProductAsync(model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteSingleProduct(string id)
        {
            await productService.DeleteSingleProductAsync(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteProduct(string id)
        {
            await productService.DeleteProduct(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var types = await productService.GetAllTypesAsync();
            var models = await productService.GetAllModelsAsync();
            var brands = await productService.GetAllBrandsAsync();

            ViewBag.Types = types;
            ViewBag.Models = models;
            ViewBag.Brands = brands;

            var model = await productService.GetForEditAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await productService.EditAsync(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
