namespace SkiShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ActionConstraints;
    using SkiShop.Core.Contracts;
    using SkiShop.Core.Models.ProductViewModels;
    using static SkiShop.Core.Constants.RoleConstants;

    [Authorize(Roles = Administrator)]
    public class AdministratorController : BaseController
    {
        private readonly IAddProductService addProductService;
        private readonly IProductService productService;

        public AdministratorController(IAddProductService _service, IProductService _productService)
        {
            addProductService = _service;
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
            var types = await addProductService.GetAllTypesAsync();
            var models = await addProductService.GetAllModelsAsync();
            var brands = await addProductService.GetAllBrandsAsync();

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

            await addProductService.AddNewProductAsync(model);

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
    }
}
