namespace SkiShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static SkiShop.Data.Common.DataConstants.Comment;

    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}