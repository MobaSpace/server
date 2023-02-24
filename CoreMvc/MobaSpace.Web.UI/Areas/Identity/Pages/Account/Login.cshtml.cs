using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MobaSpace.Core.Data.Models;
using Microsoft.Extensions.Configuration;

namespace MobaSpace.Web.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IConfiguration _config;


        public LoginModel(SignInManager<User> signInManager, 
            ILogger<LoginModel> logger, IConfiguration config,
            UserManager<User> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _config = config;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Identifiant")]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name ="Mot de passe")]
            public string Password { get; set; }

            [Display(Name = "Se souvenir de moi?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            await this.SetExternalLogins();

            ReturnUrl = returnUrl;
        }

        private async Task SetExternalLogins()
        {
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).Where(shm => !shm.Name.Contains("Api")).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            await this.SetExternalLogins();

            if (ModelState.IsValid)
            {
                var user = await _signInManager.UserManager.FindByNameAsync(Input.Username);
                var stringTest = DateTime.Parse("0001/01/01");
                if (user != null) { 
                    if (user.DatePasswordInv == null || user.DatePasswordInv == DateTime.Parse("0001/01/01"))
                    {
                        if (_config.GetValue<int>("Timers:PasswordLife") == 0)
                        {
                            user.DatePasswordInv = DateTime.Now.AddDays(30);
                        }
                        else
                        {
                            user.DatePasswordInv = DateTime.Now.AddDays(_config.GetValue<int>("Timers:PasswordLife"));
                        }

                        await _userManager.UpdateAsync(user);
                    }
                    if (user.DatePasswordInv > DateTime.Now)
                    {
                        // This doesn't count login failures towards account lockout
                        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                        var result = await _signInManager.PasswordSignInAsync(user, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            user.LastConnection = DateTime.UtcNow;
                            await _signInManager.UserManager.UpdateAsync(user);
                            _logger.LogInformation($"{user.UserName} logged in.");
                            return LocalRedirect(returnUrl);
                        }
                        if (result.RequiresTwoFactor)
                        {
                            return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                        }
                        if (result.IsLockedOut)
                        {
                            _logger.LogWarning("User account locked out.");
                            return RedirectToPage("./Lockout");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Echec de connexion.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Echec de connexion.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Echec de connexion.");
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
