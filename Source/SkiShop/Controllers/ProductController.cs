namespace SkiShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SkiShop.Core.Contracts.ProductContracts;
    using SkiShop.Core.Models.ProductViewModels;

    using SkiShop.Extension;

    public class ProductController : BaseController
    {
        private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await productService.GetAllProductsAsync();

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ShowProduct(string id)
        {
            var model = await productService.GetProductByIdAsync(id);

            return View(model);
        }

        public async Task<IActionResult> ByType(string type)
        {
            var model = await productService.GetAllProductsByTypeAsync(type);

            return View(model);
        }

        public async Task<IActionResult> AddComment(ProductViewModel model, string id)
        {
            if (!ModelState.IsValid)
            {
                return Redirect($"/Product/ShowProduct/{id}");
            }

            var userId = User.Id();

            await productService.AddNewComment(model.Comment.Description, id, userId);

            return Redirect($"/Product/ShowProduct/{id}");
        }
    }
}