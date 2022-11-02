namespace SkiShop.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using static SkiShop.Core.Constants.RoleConstants;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Area("Admin")]
    [Authorize(Roles = Administrator)]
    public class BaseController : Controller
    {
    }
}
