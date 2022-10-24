namespace SkiShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static SkiShop.Data.Common.DataConstants.Snowboard;
    public class Snowboard
    {
        public Snowboard()
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

        public ICollection<Comment> Comments { get; set; }

        [Required]
        public int Quantity { get; set; }

        public bool IsDeleted { get; set; }
    }
}