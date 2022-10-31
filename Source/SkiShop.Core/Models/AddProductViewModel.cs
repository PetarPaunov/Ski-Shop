namespace SkiShop.Core.Models
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static SkiShop.Data.Constants.DataConstants;

    public class AddProductViewModel
    {
        [Required]
        [StringLength(Product.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(Product.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please choose front image")]
        [NotMapped]
        public IFormFile FrontImage { get; set; }

        [Required]
        [Range(typeof(decimal), Product.PriceMinValue, Product.PriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        public Guid BrandId { get; set; }

        [Required]
        public Guid TypeId { get; set; }

        [Required]
        public Guid ModelId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
