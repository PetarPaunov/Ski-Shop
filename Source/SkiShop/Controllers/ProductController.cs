﻿namespace SkiShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SkiShop.Core.Contracts;
    using SkiShop.Core.Contracts.ProductContracts;
    using SkiShop.Core.Models.ProductViewModels;

    using SkiShop.Extension;

    public class ProductController : BaseController
    {
        private readonly IProductService productService;
        private readonly IProductServiceAdmin productServiceAdmin;
        public ProductController(IProductService _productService, IProductServiceAdmin _productServiceAdmin)
        {
            productService = _productService;
            productServiceAdmin = _productServiceAdmin;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int currentPage = 1, string? type = null, string? searchTerm = null)
        {
            ViewBag.Types = await productServiceAdmin.GetAllTypesAsync();

            var model = await productService.GetAllProductsAsync(currentPage, type, searchTerm);  

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ShowProduct(string id)
        {
            var model = await productService.GetProductByIdAsync(id);

            return View(model);
        }

        public async Task<IActionResult> ByType(string type, int currentPage = 1)
        {
            var model = await productService.GetAllProductsByTypeAsync(type, currentPage);

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