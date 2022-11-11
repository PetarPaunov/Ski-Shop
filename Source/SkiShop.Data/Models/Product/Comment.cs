namespace SkiShop.Data.Models.Product
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static SkiShop.Data.Constants.DataConstants.Comment;

    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}