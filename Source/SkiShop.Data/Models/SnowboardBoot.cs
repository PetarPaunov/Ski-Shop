﻿namespace SkiShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static SkiShop.Data.Common.DataConstants.SnowboardBoot;

    public class SnowboardBoot
    {
        public SnowboardBoot()
        {
            Comments = new HashSet<Comment>();
        }

        [Key]
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
        [Range(SizeMinValue, SizeMaxValue)]
        public double Size { get; set; }

        [Required]
        public string RetentionSystem { get; set; }

        [Required]
        public string Soles { get; set; }

        public string BrandId { get; set; }

        public Brand Brand { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public int Quantity { get; set; }

        public bool IsDeleted { get; set; }
    }
}