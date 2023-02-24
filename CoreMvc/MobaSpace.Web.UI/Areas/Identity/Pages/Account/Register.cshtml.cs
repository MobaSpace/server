
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MobaSpace.Core.Data;
using MobaSpace.Core.Data.Models;
using MobaSpace.Core.Email;
using MobaSpace.Core.Utils;
using MobaSpace.Web.UI.ViewModels;
using static System.Net.WebRequestMethods;

namespace MobaSpace.Web.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;

        public RegisterModel(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public List<Role> UserRoles = new List<Role>(Roles.GetRoles().Select(r => new Role() { Nom = r }));



        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Le champ {0} doit être rempli")]
            [Display(Name = "Prenom de l'utilisateur")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Le champ {0} doit être rempli")]
            [Display(Name = "Nom de l'utilisateur")]
            public string UserSurname { get; set; }

            [Required(ErrorMessage = "Le champ {0} doit être rempli")]
            [EmailAddress(ErrorMessage = "L'adresse e-mail rentrée n'est pas valide.")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Le champ {0} doit être rempli")]
            [StringLength(100, ErrorMessage = "Le {0} doit être au minimum de {2} et au maximum de {1} caractères.", MinimumLength = 4)]
            [DataType(DataType.Password)]
            [Display(Name = "Mot de passe")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmation mot de passe")]
            [Compare("Password", ErrorMessage = "Le {0} et la confirmation ne correspondent pas.")]
            public string ConfirmPassword { get; set; }

            [Phone]
            [DataType(DataType.PhoneNumber)]
            [RegularExpression("^(?:(?:\\+|00)33|0|33)\\s*[1-9](?:[\\s.-]*\\d{2}){4}$", ErrorMessage = "Le format du numéro n'est pas valide")]
            [Display(Name = "Numero appel d'alarme")]
            public string AppelPhoneNumber { get; set; }

            [Display(Name = "Canal de Notification Android : ")]
            public string CanalNotif { get; set; }

            [Display(Name = "Rattacher à NetSOINS")]
            public bool Linked2NetSOINS { get; set; }

            public List<Role> UserRoles { get; set; } = new List<Role>(Roles.GetRoles().Select(r => new Role() { Nom = r }));
        }


        public async Task<IActionResult> OnPostAsync(
            [FromServices] UserManager<User> userManager,
            [FromServices] RoleManager<IdentityRole> roleManager,
            [FromServices] ILogger<RegisterModel> logger,
            [FromServices] EmailSender emailSender,
            [FromServices] IOptions<EmailSettings> mailSettings,
            [FromServices] IOptions<WebServerSettings> webServerSettings,
            [FromServices] IFileProvider fileProvider,
            [FromServices] IHttpClientFactory clientFactory, 
            [FromServices] MobaDbContext context, 
            string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                Input.AppelPhoneNumber = changeFormat(Input.AppelPhoneNumber);
                try
                {
                    var user = new User { UserName = Input.UserName, Email = Input.Email, EmailConfirmed = true, Creation = DateTime.UtcNow, LastConnection = DateTime.UtcNow , Appel = Input.AppelPhoneNumber , Linked2NetSOINS = Input.Linked2NetSOINS , CanalNotif = Input.CanalNotif };
                    var result = await userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        logger.LogInformation("User created a new account with password.");

                        foreach (var role in Roles.GetRoles())
                        {
                            if (!await roleManager.RoleExistsAsync(role))
                            {
                                await roleManager.CreateAsync(new IdentityRole(role));
                            }
                        };

                        foreach(Role role in Input.UserRoles)
                        {
                            if (role.Selectionne)
                            {
                                await userManager.AddToRoleAsync(user, role.Nom);
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "L'utilisateur existe déjà !");
                        goto err;
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    Response.Redirect("/Users/index");
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "Impossible de créer un utilisateur !");
                    goto err;
                }
                
                
            }
        err:
            // If we got this far, something failed, redisplay form
            return Page();
        }

        public string changeFormat(string phoneNumber)
        {
            string internationnalformat = phoneNumber;
            if (phoneNumber != null)
            {
                if (phoneNumber.StartsWith("00"))
                {
                    internationnalformat = phoneNumber.Remove(0, 2);
                }
                else if (phoneNumber.StartsWith('0'))
                {
                    var regex = new Regex(Regex.Escape("0"));
                    internationnalformat = regex.Replace(phoneNumber, "33", 1);

                }
                else if (phoneNumber.StartsWith("+33"))
                {
                    internationnalformat = phoneNumber.Remove(0, 1);

                }
                else if (phoneNumber.StartsWith("33"))
                {
                    internationnalformat = phoneNumber;
                }
                else
                {
                    internationnalformat = null;
                }
            }
            return internationnalformat;
        }
    }
}
