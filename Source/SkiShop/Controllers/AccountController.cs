namespace SkiShop.Controllers
{
    using System.Security.Claims;
    using SkiShop.Core.Constants;
    using Microsoft.AspNetCore.Mvc;
    using SkiShop.Data.Models.Account;
    using SkiShop.Core.Contracts.Email;
    using Microsoft.AspNetCore.Identity;
    using SkiShop.Data.Models.ShoppingCart;
    using SkiShop.Models.AccountViewModels;
    using Microsoft.AspNetCore.Authorization;
    using SkiShop.Core.Models.EmailViewModels;
    using SkiShop.Core.Contracts.ShoppingCart;

    using static SkiShop.Core.Constants.ToastrMessagesConstants;
    using static SkiShop.Core.Constants.ErrorMessagesConstants;

    public class AccountController : BaseController
    {
        private readonly IEmailService emailService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IShoppingCartService shoppingCartService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(SignInManager<ApplicationUser> _signInManager, 
                                 UserManager<ApplicationUser> _userManager,
                                 RoleManager<IdentityRole> _roleManager,
                                 IEmailService _emailService,
                                 IShoppingCartService _shoppingCartService)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            roleManager = _roleManager;
            emailService = _emailService;
            shoppingCartService = _shoppingCartService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirect = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirect);
            return new ChallengeResult(provider, properties);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"{ErrorFromExternalProvider} {remoteError}");

                return RedirectToAction("Login", new { ReturnUrl = returnUrl });
            }

            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, ExternalLoginInformationLost);

                return RedirectToAction("Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                await signInManager.UpdateExternalAuthenticationTokensAsync(info);
                return LocalRedirect(returnUrl);
            }
            else
            {
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["ProviderDisplayName"] = info.ProviderDisplayName;

                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View(nameof(ExternalLoginConfirmation), new ExternalLoginViewModel { Email = email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var info = await signInManager.GetExternalLoginInfoAsync();

                if (info == null)
                {
                    return View("Error");
                }

                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    PhoneNumber = model.PhoneNumber,
                    EmailConfirmed = true
                };

                user.ShoppingCart = new ShoppingCart() { UserId = user.Id };

                var result = await userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, RoleConstants.User);
                    result = await userManager.AddLoginAsync(user, info);

                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        await signInManager.UpdateExternalAuthenticationTokensAsync(info);

                        return LocalRedirect(returnUrl);
                    }
                }

                ModelState.AddModelError("Email", UserExist);
            }


            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

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
                PhoneNumber = model.PhoneNumber,
            };

            user.ShoppingCart = new ShoppingCart() { UserId = user.Id };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var roleExist = await roleManager.RoleExistsAsync(RoleConstants.User);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(RoleConstants.User));
                }

                await userManager.AddToRoleAsync(user, RoleConstants.User);

                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationlink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);
                var message = new Message(new string[] { user.Email }, "Confirmation email link", confirmationlink!);

                emailService.SendEmail(message);

                TempData[MessageConstant.WarningMessage] = ConfirmYourEmail;

                return RedirectToAction(nameof(Login));
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
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

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

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, model.IsPersistent, false);

                if (result.Succeeded)
                {
                    var productsCount = await shoppingCartService.CartProductsCoutAsync(user.Id);
                    HttpContext.Session.SetInt32("ProductsCount", productsCount);

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, SomthingWentWrong);

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                {
                    TempData.Clear();
                    TempData[MessageConstant.SuccessMessage] = EmailConfirmt;
                    return RedirectToAction(nameof(Login));
                }
            }

            ModelState.AddModelError(string.Empty, UserDoesNotExist);
            return RedirectToAction(nameof(Register));
        }
    }
}