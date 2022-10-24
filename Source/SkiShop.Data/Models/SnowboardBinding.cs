namespace SkiShop.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    using static SkiShop.Data.Common.DataConstants.SnowboardBinding;

    public class SnowboardBinding
    {
        public SnowboardBinding()
        {
            Comments = new HashSet<Comment>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Range(typeof(decimal), PriceMaxValue, PriceMinValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(SizeMinValue, SizeMaxValue)]
        public double Size { get; set; }

        [Required]
        public int Flex { get; set; }

        [Required]
        public string AnkleStrap { get; set; }

        [Required]
        public string ToeStrap { get; set; }

        [Required]
        [ForeignKey(nameof(Brand))]
        public Guid BrandId { get; set; }

        public Brand Brand { get; set; }

        public ICollection<Comment> Comments { get; set; }

        [Required]
        public int Quantity { get; set; }

        public bool IsDeleted { get; set; }
    }
}