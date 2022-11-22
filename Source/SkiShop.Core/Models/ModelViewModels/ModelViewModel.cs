namespace SkiShop.Core.Models.ModelViewModels
{
    /// <summary>
    /// View model for extracting product models from the database
    /// </summary>
	public class ModelViewModel
	{
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
    }
}