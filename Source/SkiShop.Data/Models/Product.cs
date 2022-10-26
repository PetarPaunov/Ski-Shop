namespace SkiShop.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    using static SkiShop.Data.Constants.DataConstants.Product;

    public class Product
    {
        public Product()
        {
            this.ProductComments = new HashSet<ProductCommet>();
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

        [Required]
        [Range(typeof(decimal), PriceMaxValue, PriceMinValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(NoseWidthMinValue, NoseWidthMaxValue)]
        public double NoseWidth { get; set; }

        [Required]
        [Range(WaistWidthMinValue, WaistWidthMaxValue)]
        public double WaistWidth { get; set; }

        [Required]
        [Range(TailWidthMinValue, TailWidthMaxValue)]
        public double TailWidth { get; set; }

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