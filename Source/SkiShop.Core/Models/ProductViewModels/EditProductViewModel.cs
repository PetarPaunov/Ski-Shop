namespace SkiShop.Core.Models.ProductViewModels
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using static SkiShop.Data.Constants.DataConstants;

    /// <summary>
    /// View model for extracting product for edit
    /// </summary>
    public class EditProductViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(Product.TitleMaxLength, MinimumLength = Product.TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(Product.DescriptionMaxLength, MinimumLength = Product.DescriptionMinLength)]
        public string Description { get; set; } = null!;

        public IFormFile? FrontImage { get; set; }

        [Required]
        public Guid ModelId { get; set; }

        [Required]
        public Guid TypeId { get; set; }

        [Required]
        public Guid BrandId { get; set; }

        [Required]
        [Range(typeof(decimal), Product.PriceMinValue, Product.PriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(Product.QuantityMinValue, Product.QuantityMaxValue)]
        public int Quantity { get; set; }

        public string? ImageUrl { get; set; }
    }
}