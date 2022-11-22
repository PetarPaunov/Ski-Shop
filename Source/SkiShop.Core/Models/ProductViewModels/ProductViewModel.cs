namespace SkiShop.Core.Models.ProductViewModels
{
    using SkiShop.Core.Models.CommentViewModels;

    /// <summary>
    /// View model for extraction products with comments from the database
    /// </summary>
    public class ProductViewModel
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Model { get; set; }
        public string? Type { get; set; }
        public string? Brand { get; set; }
        public string? Price { get; set; }
        public int? Quantity { get; set; }
        public string? ImageUrl { get; set; }
        public IEnumerable<CommentViewModel>? Comments { get; set; }
        public AddCommentViewModel? Comment { get; set; }
    }
}