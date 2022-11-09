namespace SkiShop.Data.Models.Product
{
    using SkiShop.Data.Models.Account;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static SkiShop.Data.Constants.DataConstants.Comment;

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