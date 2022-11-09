namespace SkiShop.Data.Models.Product
{
    using SkiShop.Data.Models.Account;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserComment
    {
        public string ApplicationUserId { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }

        public Guid CommentId { get; set; }

        [ForeignKey(nameof(CommentId))]
        public Comment Comment { get; set; }
    }
}
