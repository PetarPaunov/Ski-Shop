namespace SkiShop.Core.Models.CommentViewModels
{
    using System.ComponentModel.DataAnnotations;
    using static SkiShop.Data.Constants.DataConstants.Comment;

    public class AddCommentViewModel
    {
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }
    }
}