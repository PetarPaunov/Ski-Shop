namespace SkiShop.Data.Models.Product
{
    using SkiShop.Data.Models.Account;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Mapping table for product and user
    /// </summary>
    public class UserComment
    {
        /// <summary>
        /// Id of the user 
        /// </summary>
        public string ApplicationUserId { get; set; } = null!;

        /// <summary>
        /// Reference to the actual user
        /// </summary>
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;

        /// <summary>
        /// Id of the comment
        /// </summary>
        public Guid CommentId { get; set; }

        /// <summary>
        /// Reference to the actual comment
        /// </summary>
        [ForeignKey(nameof(CommentId))]
        public Comment Comment { get; set; } = null!;
    }
}