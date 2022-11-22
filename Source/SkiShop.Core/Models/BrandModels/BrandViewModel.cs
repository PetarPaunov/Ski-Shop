namespace SkiShop.Core.Models.BrandModels
{
	/// <summary>
	/// View model for extracting brands from the database
	/// </summary>
	public class BrandViewModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
	}
}