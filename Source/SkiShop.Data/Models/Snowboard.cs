namespace SkiShop.Data.Models
{
    public class Snowboard
    {
        public Snowboard()
        {
            Comments = new HashSet<Comment>();
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public double NoseWidth { get; set; }

        public double TailWidth { get; set; }

        public string BrandId { get; set; }

        public Brand Brand { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public int Quantity { get; set; }

        public bool IsDeleted { get; set; }

    }
}
