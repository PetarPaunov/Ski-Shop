namespace SkiShop.Data.Models.ProductCommon
{
    using System.ComponentModel.DataAnnotations;

    public class Attribute
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
