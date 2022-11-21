namespace SkiShop.Data.Models.Product
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static SkiShop.Data.Constants.DataConstants.Comment;

    /// <summary>
    /// Comment of a product
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Description for the comment
        /// </summary>
        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        /// <summary>
        /// Date of creation of the comment
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Boolean flag indicating whether the brand has been deleted
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}