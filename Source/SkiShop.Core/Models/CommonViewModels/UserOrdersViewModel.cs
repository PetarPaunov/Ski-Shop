namespace SkiShop.Core.Models.CommonViewModels
{
    /// <summary>
    /// View model for extracting user orders from the database
    /// </summary>
    public class UserOrdersViewModel
    {
        public string OrderId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ProductTitle { get; set; } = null!;   
        public string ProductPrice { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public int Quantity { get; set; }

    }
}