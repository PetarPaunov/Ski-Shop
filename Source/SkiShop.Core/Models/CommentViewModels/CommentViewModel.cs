namespace SkiShop.Core.Models.CommentViewModels
{
    /// <summary>
    /// View model for extracting comments from the database
    /// </summary>
    public class CommentViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string CreateOn { get; set; } = null!;

        public string User { get; set; } = null!;
    }
}