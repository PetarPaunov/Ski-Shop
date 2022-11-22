namespace SkiShop.Controllers
{
    using SkiShop.Extension;
    using SkiShop.Core.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using SkiShop.Core.Models.ProductViewModels;
    using SkiShop.Core.Contracts.ProductContracts;

    public class ProductController : BaseController
    {
        private readonly IProductService productService;
        private readonly IProductServiceAdmin productServiceAdmin;
        private readonly ILogger<ProductController> logger;
        public ProductController(IProductService _productService, 
                                 IProductServiceAdmin _productServiceAdmin,
                                 ILogger<ProductController> _logger)
        {
            productService = _productService;
            productServiceAdmin = _productServiceAdmin;
            logger = _logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index([FromQuery] ProductPagingViewModel query)
        {
            ViewBag.Types = await productServiceAdmin.GetAllTypesAsync();

            var result = await productService.GetAllProductsAsync
                 (
                    query.CurrentPage,
                    query.ProductsPerPage,
                    query.Type,
                    query.SearchTerm
                 );

            query.TotalProductsCount = result.TotalProductsCount;
            query.Products = result.Products;

            return View(query);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ShowProduct(string id)
        {
            try
            {
                var model = await productService.GetProductByIdAsync(id);

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }

        public async Task<IActionResult> AddComment(ProductViewModel model, string id)
        {
            if (!ModelState.IsValid)
            {
                return Redirect($"/Product/ShowProduct/{id}");
            }

            var userId = User.Id();

            try
            {
                await productService.AddNewComment(model.Comment.Description, id, userId);

                return Redirect($"/Product/ShowProduct/{id}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }
    }
}