namespace SkiShop.Data.Models.Product
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Mapping table for product and comment
    /// </summary>
    public class ProductComment
    {
        /// <summary>
        /// Id of the comment 
        /// </summary>
        public Guid CommentId { get; set; }

        /// <summary>
        /// Reference to the actual comment
        /// </summary>
        [ForeignKey(nameof(CommentId))]
        public Comment Comment { get; set; } = null!;

        /// <summary>
        /// Id of the product 
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Reference to the actual product
        /// </summary>
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        /// <summary>
        /// Name of the user posted the comment
        /// </summary>
        public string UserName { get; set; } = null!;
    }
}