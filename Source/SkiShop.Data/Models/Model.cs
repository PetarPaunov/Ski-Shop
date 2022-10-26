namespace SkiShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Model
    {
        public Model()
        {
            this.Products = new HashSet<Product>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
