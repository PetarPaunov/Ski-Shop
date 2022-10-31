namespace SkiShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SkiShop.Core.Contracts;
    using SkiShop.Core.Models;
    using static SkiShop.Core.Constants.RoleConstants;

    [Authorize(Roles = Administrator)]
    public class AdministratorController : BaseController
    {
        private readonly IAddProductService addProductService;

        public AdministratorController(IAddProductService _service)
        {
            addProductService = _service;
        }

        public IActionResult Index()
        {
            return View();
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
    }
}
