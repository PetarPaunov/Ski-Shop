using System.ComponentModel.DataAnnotations;

namespace SkiShop.Core.Models.CommonViewModels
{
	public class AddOthersViewModel
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public string KeyWord { get; set; }
	}
}
