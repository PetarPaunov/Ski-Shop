namespace SkiShop.Core.Models.UserViewModels
{
	/// <summary>
	/// View model for extraction users from the database (Administration)
	/// </summary>
    public class UserViewModel
	{
		public string Id { get; set; } = null!;
		public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IEnumerable<string> Roles { get; set; } = null!;
    }
}