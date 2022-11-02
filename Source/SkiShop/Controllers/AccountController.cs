namespace SkiShop.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using SkiShop.Models.AccountViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SkiShop.Data.Models.Account;

    using static SkiShop.Core.Constants.RoleConstants;

    public class AccountController : BaseController
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(SignInManager<ApplicationUser> _signInManager, 
                                    UserManager<ApplicationUser> _userManager,
                                    RoleManager<IdentityRole> _roleManager)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            roleManager = _roleManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(String.Empty, error.Description);
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel();

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    var isInRole = await userManager.IsInRoleAsync(user, "Administrator");
                    if (isInRole)
                    {
                        return Redirect("~/admin");
                    }
                    
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Something went wrong!");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        // Should be moved to administrator controller
        public async Task<IActionResult> CreateRole()
        {
            await roleManager.CreateAsync(new IdentityRole(Administrator));

            return RedirectToAction("Index", "Home");
        }

        // Should be moved to administrator controller
        public async Task<IActionResult> AddToRole()
        {
            var email = "petar_dp.2000@abv.bg";

            var user = await userManager.FindByEmailAsync(email);

            await userManager.AddToRoleAsync(user, Administrator);

            return RedirectToAction("Index", "Home");
        }
    }
}
