namespace SkiShop.Areas.Admin.Controllers
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

		public async Task<IActionResult> AddToRole(string email, string role)
		{
			await roleServiceAdmin.AddToRoleAsync(email, role);

			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> RoleManaging()
		{
			var roles = await roleServiceAdmin.GetAllRolesAsync();

			ViewBag.Roles = roles;

			var model = new RoleViewModel();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> AddRole(RoleViewModel model)
		{
			if (!ModelState.IsValid)
			{
                var roles = await roleServiceAdmin.GetAllRolesAsync();
                ViewBag.Roles = roles;

                return View(model);
			}

			await roleServiceAdmin.CreateRoleAsync(model.Name);

			return RedirectToAction(nameof(RoleManaging));
		}

		[HttpPost]
		public async Task<IActionResult> RemoveRole(RoleViewModel model)
		{
            if (!ModelState.IsValid)
            {
                return View(model);
            }

			await roleServiceAdmin.DeleteRoleAsync(model.Name);

            return RedirectToAction(nameof(RoleManaging));
        }

		public async Task<IActionResult> UserOrders()
		{
			var orders = await userServiceAdmin.GetAllUserOrdersAsync();

			return View(orders);
		}

		public async Task<IActionResult> FinishOrder(string id)
		{
			await userServiceAdmin.FinishUserOrderAsync(id);

			return RedirectToAction(nameof(UserOrders));
		}
    }
}