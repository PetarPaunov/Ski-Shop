using System.ComponentModel.DataAnnotations;

namespace SkiShop.Data.Models
{
    public class Brand
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

    }
}
