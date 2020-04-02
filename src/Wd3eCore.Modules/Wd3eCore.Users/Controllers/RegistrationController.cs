using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Wd3eCore.DisplayManagement.Notify;
using Wd3eCore.Email;
using Wd3eCore.Entities;
using Wd3eCore.Modules;
using Wd3eCore.Settings;
using Wd3eCore.Users.Models;
using Wd3eCore.Users.ViewModels;

namespace Wd3eCore.Users.Controllers
{
    [Feature("Wd3eCore.Users.Registration")]
    public class RegistrationController : Controller
    {
        private readonly UserManager<IUser> _userManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly ISiteService _siteService;
        private readonly INotifier _notifier;
        private readonly IEmailAddressValidator _emailAddressValidator;
        private readonly ILogger _logger;
        private readonly IStringLocalizer<RegistrationController> S;
        private readonly IHtmlLocalizer<RegistrationController> H;

        public RegistrationController(
            UserManager<IUser> userManager,
            IAuthorizationService authorizationService,
            ISiteService siteService,
            INotifier notifier,
            IEmailAddressValidator emailAddressValidator,
            ILogger<RegistrationController> logger,
            IHtmlLocalizer<RegistrationController> htmlLocalizer,
            IStringLocalizer<RegistrationController> stringLocalizer)
        {
            _userManager = userManager;
            _authorizationService = authorizationService;
            _siteService = siteService;
            _notifier = notifier;
            _emailAddressValidator = emailAddressValidator ?? throw new ArgumentNullException(nameof(emailAddressValidator));
            _logger = logger;
            H = htmlLocalizer;
            S = stringLocalizer;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            var settings = (await _siteService.GetSiteSettingsAsync()).As<RegistrationSettings>();
            if (settings.UsersCanRegister != UserRegistrationType.AllowRegistration)
            {
                return NotFound();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            var settings = (await _siteService.GetSiteSettingsAsync()).As<RegistrationSettings>();

            if (settings.UsersCanRegister != UserRegistrationType.AllowRegistration)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(model.Email) && !_emailAddressValidator.Validate(model.Email))
            {
                ModelState.AddModelError("Email", S["Invalid email."]);
            }

            ViewData["ReturnUrl"] = returnUrl;

            // If we get a user, redirect to returnUrl
            if (await this.RegisterUser(model, S["Confirm your account"], _logger) != null)
            {
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(RegistrationController.Register), "Registration");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return View();
            }

            return NotFound();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendVerificationEmail(string id)
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.ManageUsers))
            {
                return Forbid();
            }

            var user = await _userManager.FindByIdAsync(id) as User;
            if (user != null)
            {
                await this.SendEmailConfirmationTokenAsync(user, S["Confirm your account"]);

                _notifier.Success(H["Verification email sent."]);
            }

            return RedirectToAction(nameof(AdminController.Index), "Admin");
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
