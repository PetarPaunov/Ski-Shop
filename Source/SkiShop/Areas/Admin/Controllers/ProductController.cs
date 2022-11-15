namespace SkiShop.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SkiShop.Core.Contracts;
    using SkiShop.Core.Models.CommonViewModels;
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

        [HttpGet]
        public async Task<IActionResult> Deleted()
        {
            var model = await productService.GetAllDeletedProductsAsync();

            return View(model);
        }

        public async Task<IActionResult> ReturnProduct(string id, int quantity)
        {
            await productService.ReturnDeletedProduct(id, quantity);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AddOthers()
        {
            var types = await productService.GetAllTypesAsync();
            var models = await productService.GetAllModelsAsync();
            var brands = await productService.GetAllBrandsAsync();

            ViewBag.Types = types;
            ViewBag.Models = models;
            ViewBag.Brands = brands;

            var model = new AddOthersViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOthers(AddOthersViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await productService.AddOthers(model.KeyWord, model.Name);

            return RedirectToAction(nameof(AddOthers));
        }
    }
}
