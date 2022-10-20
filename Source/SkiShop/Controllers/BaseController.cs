using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SkiShop.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
