namespace SkiShop.Data.Models.ProductCommon
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    using static SkiShop.Data.Constants.DataConstants.Product;
    using Microsoft.AspNetCore.Http;

    public class Product
    {
        public Product()
        {
            ProductComments = new HashSet<ProductCommet>();
            ProductAttributes = new HashSet<ProductAttribute>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Please choose front image")]
        [NotMapped]
        public IFormFile FrontImage { get; set; }

        [Required]
        [Range(typeof(decimal), PriceMaxValue, PriceMinValue)]
        public decimal Price { get; set; }

        public ICollection<ProductAttribute> ProductAttributes { get; set; }

        [Required]
        [ForeignKey(nameof(Brand))]
        public Guid BrandId { get; set; }

        public Brand Brand { get; set; }

        [Required]
        [ForeignKey(nameof(Type))]
        public Guid TypeId { get; set; }

        public Type Type { get; set; }

        [Required]
        [ForeignKey(nameof(Model))]
        public Guid ModelId { get; set; }

        public Model Model { get; set; }

        public ICollection<ProductCommet> ProductComments { get; set; }

        [Required]
        public int Quantity { get; set; }

        public bool IsDeleted { get; set; }
    }
}