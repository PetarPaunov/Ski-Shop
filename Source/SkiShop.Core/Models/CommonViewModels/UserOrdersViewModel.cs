namespace SkiShop.Core.Models.CommonViewModels
{
    public class UserOrdersViewModel
    {
        public string OrderId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ProductTitle { get; set; }
        public string ProductPrice { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }

    }
}
