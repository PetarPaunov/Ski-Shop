namespace SkiShop.Core.Models.CommentViewModels
{
    using System.ComponentModel.DataAnnotations;
    using static SkiShop.Data.Constants.DataConstants.Comment;

    /// <summary>
    /// View model for adding a comment to the database
    /// </summary>
    public class AddCommentViewModel
    {
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;
    }
}