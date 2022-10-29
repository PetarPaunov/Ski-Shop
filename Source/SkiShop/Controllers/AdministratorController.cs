namespace SkiShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SkiShop.Core.Models;
    using static SkiShop.Core.Constants.RoleConstants;

    [Authorize(Roles = Administrator)]
    public class AdministratorController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            var model = new AddProductViewModel();

            return View(model);
        }
    }
}
