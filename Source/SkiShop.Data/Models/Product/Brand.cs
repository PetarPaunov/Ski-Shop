namespace SkiShop.Data.Models.Product
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Brand of the products
    /// </summary>
    public class Brand
    {
        public Brand()
        {
            Products = new HashSet<Product>();
        }

        /// <summary>
        /// Unique identifier
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the current brand
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Boolean flag indicating whether the brand has been deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// A collection of products
        /// </summary>
        public ICollection<Product> Products { get; set; }
    }
}