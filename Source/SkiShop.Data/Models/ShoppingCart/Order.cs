namespace SkiShop.Data.Models.ShoppingCart
{
    using SkiShop.Data.Models.Account;
    using SkiShop.Data.Models.Product;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Order
    {
        public Guid Id { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }

        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
