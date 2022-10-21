namespace SkiShop.Data.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}
