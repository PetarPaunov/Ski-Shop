﻿namespace SkiShop.Core.Models.UserViewModels
{
    using SkiShop.Core.Models.RoleViewModels;


    public class UserViewModel
	{
		public string Id { get; set; }

		public string UserName { get; set; }

		public string Email { get; set; }

		public string? Role  { get; set; }

		public IEnumerable<string> Roles { get; set; }
	}
}
