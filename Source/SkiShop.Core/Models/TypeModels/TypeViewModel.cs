namespace SkiShop.Core.Models.TypeModels
{
    /// <summary>
    /// View model for extracting product types from the database
    /// </summary>
	public class TypeViewModel
	{
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}