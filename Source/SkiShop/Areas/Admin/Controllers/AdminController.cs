﻿namespace SkiShop.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using SkiShop.Core.Contracts.Admin;
	using SkiShop.Core.Models.RoleViewModels;

	public class AdminController : BaseController
	{
		private readonly IUserServiceAdmin userServiceAdmin;
		private readonly IRoleServiceAdmin roleServiceAdmin;

		public AdminController(IUserServiceAdmin _userServiceAdmin, 
			   IRoleServiceAdmin _roleServiceAdmin)
		{
			userServiceAdmin = _userServiceAdmin;
			roleServiceAdmin = _roleServiceAdmin;
		}

		public async Task<IActionResult> Index()
		{
			ViewBag.Roles = await roleServiceAdmin.GetAllRolesAsync();

			var model = await userServiceAdmin.GetAllUsersAsync();

			return View(model);
		}

		public async Task<IActionResult> Details(string id)
		{
			throw new NotImplementedException(); //TODO: User Details
		}

		public async Task<IActionResult> AddToRole(string email, string role)
		{
			await roleServiceAdmin.AddToRoleAsync(email, role);
			// TODO: Impelemt. ..
			return null;
		}

		public async Task<IActionResult> RoleManaging()
		{
			var roles = await roleServiceAdmin.GetAllRolesAsync();
			var userEmails = await userServiceAdmin.GetAllUserEmailsAsync();

			ViewBag.UserEmails = userEmails;
			ViewBag.Roles = roles;

			var model = new RoleViewModel();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> AddRole(RoleViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			await roleServiceAdmin.CreateRoleAsync(model.Name);

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> RemoveRole(RoleViewModel model)
		{
            if (!ModelState.IsValid)
            {
                return View(model);
            }

			await roleServiceAdmin.DeleteRoleAsync(model.Name);

            return RedirectToAction(nameof(Index));
        }
    }
}