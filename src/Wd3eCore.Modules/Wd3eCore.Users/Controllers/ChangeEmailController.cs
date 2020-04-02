using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Wd3eCore.DisplayManagement;
using Wd3eCore.Entities;
using Wd3eCore.Modules;
using Wd3eCore.Settings;
using Wd3eCore.Users.Models;
using Wd3eCore.Users.Services;
using Wd3eCore.Users.ViewModels;

namespace Wd3eCore.Users.Controllers
{
    [Feature("Wd3eCore.Users.ChangeEmail")]
    public class ChangeEmailController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<IUser> _userManager;
        private readonly ISiteService _siteService;
        private readonly ILogger<ChangeEmailController> _logger;
        private readonly IStringLocalizer<ChangeEmailController> S;

        public ChangeEmailController(
            IUserService userService,
            UserManager<IUser> userManager,
            ISiteService siteService,
            IDisplayHelper displayHelper,
            IStringLocalizer<ChangeEmailController> stringLocalizer,
            ILogger<ChangeEmailController> logger)
        {
            _userService = userService;
            _userManager = userManager;
            _siteService = siteService;

            S = stringLocalizer;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!(await _siteService.GetSiteSettingsAsync()).As<ChangeEmailSettings>().AllowChangeEmail)
            {
                return NotFound();
            }

            var user = await _userService.GetAuthenticatedUserAsync(User);

            return View(new ChangeEmailViewModel() { Email = ((User)user).Email });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ChangeEmailViewModel model)
        {
            if (!(await _siteService.GetSiteSettingsAsync()).As<ChangeEmailSettings>().AllowChangeEmail)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userService.GetAuthenticatedUserAsync(User);
                var userWithEmail = await _userManager.FindByEmailAsync(model.Email);

                if (((User)user).Email == model.Email)
                {
                    ModelState.AddModelError("Email", S["This email is already your current one."]);
                }
                else if (userWithEmail != null && user.UserName != userWithEmail.UserName)
                {
                    ModelState.AddModelError("Email", S["A user with the same email already exists."]);
                }
                else
                {
                    if (await _userService.ChangeEmailAsync(user, model.Email,
                        (key, message) => ModelState.AddModelError(key, message)))
                    {
                        return RedirectToLocal(Url.Action("ChangeEmailConfirmation", "ChangeEmail"));
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ChangeEmailConfirmation()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect("~/");
            }
        }
    }
}
