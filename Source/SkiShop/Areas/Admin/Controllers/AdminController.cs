namespace SkiShop.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using SkiShop.Core.Contracts.Admin;
	using SkiShop.Core.Models.RoleViewModels;

	public class AdminController : BaseController
	{
		private readonly IUserServiceAdmin userServiceAdmin;

		public AdminController(IUserServiceAdmin _userServiceAdmin)
		{
			userServiceAdmin = _userServiceAdmin;
		}

		public async Task<IActionResult> Index()
		{
			ViewBag.Roles = await userServiceAdmin.GetAllRolesAsync();

			var model = await userServiceAdmin.GetAllUsersAsync();

			return View(model);
		}

		public async Task<IActionResult> Details(string id)
		{
			throw new NotImplementedException(); //TODO: User Details
		}

		public async Task<IActionResult> AddToRole(string email, string role)
		{
			await userServiceAdmin.AddToRoleAsync(email, role);
			// TODO: Impelemt. ..
			return null;
		}

		public async Task<IActionResult> RoleManaging()
		{
			var roles = await userServiceAdmin.GetAllRolesAsync();
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

			await userServiceAdmin.CreateRoleAsync(model.Name);

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> RemoveRole(RoleViewModel model)
		{
            if (!ModelState.IsValid)
            {
                return View(model);
            }

			await userServiceAdmin.DeleteRoleAsync(model.Name);

            return RedirectToAction(nameof(Index));
        }
    }
}
