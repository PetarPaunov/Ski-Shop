namespace SkiShop.Data.Models.Product
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class ProductComment
    {
        public Guid CommentId { get; set; }

        [ForeignKey(nameof(CommentId))]
        public Comment Comment { get; set; }

        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
