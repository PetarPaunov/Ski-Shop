namespace SkiShop.Core.Models
{
    using System.ComponentModel.DataAnnotations;

    using static SkiShop.Data.Constants.DataConstants;

    public class AddProductViewModel
    {
        [Required]
        [StringLength(Product.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(Product.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Range(typeof(decimal), Product.PriceMaxValue, Product.PriceMinValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(Product.NoseWidthMinValue, Product.NoseWidthMaxValue)]
        public double NoseWidth { get; set; }

        [Required]
        [Range(Product.WaistWidthMinValue, Product.WaistWidthMaxValue)]
        public double WaistWidth { get; set; }

        [Required]
        [Range(Product.TailWidthMinValue, Product.TailWidthMaxValue)]
        public double TailWidth { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
