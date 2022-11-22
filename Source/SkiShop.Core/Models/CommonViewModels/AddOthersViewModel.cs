namespace SkiShop.Core.Models.CommonViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// View model for adding new product types, brands and models to the database
    /// </summary>
    public class AddOthersViewModel
	{
		[Required]
		public string Name { get; set; } = null!;

		[Required]
		public string KeyWord { get; set; } = null!;
	}
}