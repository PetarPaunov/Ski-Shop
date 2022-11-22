namespace SkiShop.Core.Models.RoleViewModels
{
	/// <summary>
	/// View model for extraction roles from the database
	/// </summary>
	public class RoleViewModel
	{
		public string? Id { get; set; }
		public string Name { get; set; } = null!;
	}
}