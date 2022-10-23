namespace SkiShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Brand
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

    }
}
