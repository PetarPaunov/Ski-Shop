namespace SkiShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SkiType
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string TypeName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
