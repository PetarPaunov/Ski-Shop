namespace SkiShop.Core.Models.ProductViewModels
{
    using SkiShop.Core.Models.CommentViewModels;

    public class ProductViewModel : AllProductsViewModel
    {
        public IEnumerable<CommentViewModel> Comments { get; set; }

        public AddCommentViewModel? Comment { get; set; }
    }
}
