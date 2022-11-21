namespace SkiShop.Data.Models.Product
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static SkiShop.Data.Constants.DataConstants.Product;

    /// <summary>
    /// Represents a product
    /// </summary>
    public class Product
    {
        public Product()
        {
            ProductComments = new HashSet<ProductComment>();
        }

        /// <summary>
        /// Unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The title/name of the product
        /// </summary>
        [Required]
        [StringLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        /// <summary>
        /// Description of the product
        /// </summary>
        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Image URL from cloud service
        /// </summary>
        [Required]
        public string ImageUrl { get; set; } = null!;

        /// <summary>
        /// Price of the product
        /// </summary>
        [Required]
        [Range(typeof(decimal), PriceMaxValue, PriceMinValue)]
        public decimal Price { get; set; }

        /// <summary>
        /// Id for the brand of the product
        /// </summary>
        [Required]
        [ForeignKey(nameof(Brand))]
        public Guid BrandId { get; set; }

        /// <summary>
        /// Reference to the actual brand
        /// </summary>
        public Brand Brand { get; set; } = null!;

        /// <summary>
        /// Id for the type of the product
        /// </summary>
        [Required]
        [ForeignKey(nameof(Type))]
        public Guid TypeId { get; set; }

        /// <summary>
        /// Reference to the actual type
        /// </summary>
        public Type Type { get; set; } = null!;

        /// <summary>
        /// Id for the model of the product
        /// </summary>
        [Required]
        [ForeignKey(nameof(Model))]
        public Guid ModelId { get; set; }

        /// <summary>
        /// Reference to the actual model
        /// </summary>
        public Model Model { get; set; } = null!;

        /// <summary>
        /// A collection of comments posted for a product
        /// </summary>
        public ICollection<ProductComment> ProductComments { get; set; }

        /// <summary>
        /// Quantity of the product
        /// </summary>
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// Date of creation of the product
        /// </summary>
        [Required]
        public DateTime CreateOn { get; set; }

        /// <summary>
        /// Boolean flag indicating whether the brand has been deleted
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}