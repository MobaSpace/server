using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using MobaSpace.Core.Data;
using MobaSpace.Core.Utils;
using MobaSpace.Core.Data.Datalayers;
using MobaSpace.Core.Data.Models;
using MobaSpace.Web.UI.ViewModels;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace MobaSpace.Web.UI.Controllers
{
    [Authorize(Roles = Roles.Administrateur + "," + Roles.Infirmier + "," + Roles.Soignant)]
    public class UsersController : Controller
    {
        private readonly MobaDataLayer _datalayer;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<PatientsController> _logger;
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(MobaDataLayer dataLayer, UserManager<User> userManager, ILogger<PatientsController> logger, IConfiguration config, RoleManager<IdentityRole> roleManager)
        {
            _datalayer = dataLayer;
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
            _config = config;
        }

        // GET: User
        [Authorize(Roles = Roles.Administrateur + "," + Roles.Infirmier)]
        public async Task<IActionResult> Index()
        {
            var users = await _datalayer.GetUsersAsync().ToListAsync();
            return View(users.OrderBy(user => user.UserName).Select(user => new UserRoleViewModel(user, _userManager)));
        }

        // GET: User/Details/5
        [Authorize(Roles = Roles.Administrateur + "," + Roles.Infirmier)]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var user = await _datalayer.GetUsersAsync()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(new UserRoleViewModel(user, _userManager));
        }

        // GET: User/Create
        [Authorize(Roles = Roles.Administrateur + "," + Roles.Infirmier)]
        public ActionResult Create()
        {
            var vm =  new UserRoleViewModel(new User(), _userManager);
            return View(vm);
        }

        //POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Administrateur + "," + Roles.Infirmier)]
        public async Task<ActionResult> Create(UserRoleViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }
                vm.User.Appel = ChangeFormat(vm.User.Appel);
                var user = new User { UserName = vm.User.UserName, UserFirstname = vm.User.UserFirstname, UserSurname = vm.User.UserSurname, Email = vm.User.Email, EmailConfirmed = true, Creation = DateTime.UtcNow, LastConnection = DateTime.UtcNow, Appel = vm.User.Appel, Linked2NetSOINS = vm.User.Linked2NetSOINS, CanalNotif = vm.User.CanalNotif };
                var result = await _userManager.CreateAsync(user, vm.User.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    foreach (var role in Roles.GetRoles())
                    {
                        if(!await _roleManager.RoleExistsAsync(role))
                        {
                            await _roleManager.CreateAsync(new IdentityRole(role));
                        }
                    };

                    foreach(Role role in vm.UserRoles)
                    {
                        if (role.Selectionne)
                        {
                            await _userManager.AddToRoleAsync(user, role.Nom);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "L'utilisateur existe déjà");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, "Impossible de créer un utilisteur !");
                }
                return RedirectToAction(nameof(Index));

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("ControllerError", "Impossible de créer le Utilisateur");
                _logger.LogError(ex, "Fail to create Utilisateur");
                return View(vm);
            }
        }


        // GET: User/Edit/5

        public async Task<IActionResult> Edit(string id)
        {
            if (User.IsInRole(Roles.Administrateur) || User.IsInRole(Roles.Infirmier))
            {
            }
            else
            {
                User CurrentUser = await _userManager.GetUserAsync(User);
                if (id != CurrentUser.Id)
                {
                    return NotFound();
                }
            }
            if (id == null)
            {
                return NotFound();
            }

            var user = await _datalayer.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(new UserRoleViewModel(user, _userManager));
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserRoleViewModel vm)
        {
            if (id != vm.User.Id)
            {
                return NotFound();
            }
            ModelState.Remove("User.Password");
            ModelState.Remove("User.Email");
            if (ModelState.IsValid && vm.User.ConfirmPassword == vm.User.Password)
            {
                try
                {
                    var user = await _datalayer.GetUserAsync(id);
                    if (User.IsInRole(Roles.Administrateur) || User.IsInRole(Roles.Infirmier)) { 
                        var addResult = await _userManager.AddToRolesAsync(user, vm.UserRoles.Where(ur => ur.Selectionne && !_userManager.IsInRoleAsync(user, ur.Nom).Result).Select(ur => ur.Nom));
                        if(!addResult.Succeeded)
                        {
                            ModelState.AddErrors(addResult.Errors);
                        }
                        var delResult = await _userManager.RemoveFromRolesAsync(user, vm.UserRoles.Where(ur => !ur.Selectionne && _userManager.IsInRoleAsync(user, ur.Nom).Result).Select(ur => ur.Nom));
                        if (!delResult.Succeeded)
                        {
                            ModelState.AddErrors(delResult.Errors);
                        }
                    }
                    if (vm.User.Password != user.PasswordHash && vm.User.Password != null)
                    {
                        user.PasswordHash = vm.User.Password ;
                        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, user.PasswordHash);
                        int nbJours = _config.GetValue<int>("Timers:PasswordLife") != 0 ? _config.GetValue<int>("Timers:PasswordLife") : 30;                        
                        user.DatePasswordInv = DateTime.Now.AddDays(nbJours);
                    }
                    user.UserName = vm.User.UserName;
                    user.UserFirstname = vm.User.UserFirstname;
                    user.UserSurname = vm.User.UserSurname;
                    user.Appel = ChangeFormat(vm.User.Appel);
                    user.Linked2NetSOINS = vm.User.Linked2NetSOINS;
                    user.CanalNotif = vm.User.CanalNotif;
                    await _userManager.UpdateAsync(user);
                    if (!await _userManager.IsInRoleAsync(user, "Soignant") && !await _userManager.IsInRoleAsync(user, "Infirmier"))
                    {
                       await _datalayer.DeleteContactPatientByUser(user);
                    }
                    if (ModelState.ErrorCount > 0)
                    {
                        return View(vm);
                    }
                }
                catch (Exception)
                {
                    if (!UserExists(vm.User.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        vm.User = await _datalayer.GetUserAsync(vm.User.Id);
                        return View(vm);
                    }
                }
                if (User.IsInRole(Roles.Administrateur) || User.IsInRole(Roles.Infirmier))
                {
                    return RedirectToAction(nameof(Index));
                }else
                {
                    return Redirect("/");
                }
  
            }
            vm.User = await _datalayer.GetUserAsync(vm.User.Id);
            return View(vm);
        }

        // POST: User/EditPassword/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Administrateur + "," + Roles.Infirmier)]
        public async Task<IActionResult> EditPassword(string id, string password)
        {
            try
            {
                var user = await _datalayer.GetUserAsync(id);
                user.PasswordHash = password;
                await _userManager.UpdateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
           
        }
        [Authorize(Roles = Roles.Administrateur + "," + Roles.Infirmier)]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _datalayer.GetUserAsync(id);
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Administrateur + "," + Roles.Infirmier)]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _datalayer.BeginTransactionAsync();
            await _datalayer.ClearUserIdAlarme(id);
            _datalayer.CommitTransaction();
            await _datalayer.BeginTransactionAsync();
            var test = await _datalayer.GetAlarmeByIdUserAsync(id);
            var user = await _datalayer.GetUserAsync(id);
            var result = await _userManager.DeleteAsync(user);
            
            if (!result.Succeeded)
            {
                ModelState.AddErrors(result.Errors);
                _datalayer.RollbackTransaction();
                return View(id);
            }
            _datalayer.CommitTransaction();
            return RedirectToAction(nameof(Index));
        }

        //Check if an user exists
        private bool UserExists(string id)
        {
            return _datalayer.GetUserAsync(id).Result != null;
        }

        public async Task<IActionResult> _ListUsersPartial(String LastName = null)
        {
            var SelectedUsers = new List<UserRoleViewModel>();
            if (String.IsNullOrEmpty(LastName))
            {
                var users = await _datalayer.GetUsersAsync().ToListAsync();
                SelectedUsers = users.OrderBy(user => user.UserName).Select(user => new UserRoleViewModel(user, _userManager)).ToList();
            }
            else { 
                await this._datalayer.BeginTransactionAsync();
                var users = await _datalayer.GetUsersAsync().ToListAsync();
                SelectedUsers = users.Where(u => u.UserSurname.Contains(LastName, StringComparison.OrdinalIgnoreCase) == true).Select(user => new UserRoleViewModel(user, _userManager)).ToList();
            }

            return PartialView(SelectedUsers);
        }

        //Change phone Number format
        private string ChangeFormat (string phoneNumber)
        {
            string phoneNumberFormat = phoneNumber;
            if (phoneNumber != null)
            {

                if (phoneNumber.StartsWith("00"))
                {
                    phoneNumberFormat = phoneNumber.Remove(0, 2);
                }
                else if (phoneNumber.StartsWith('0'))
                {
                    var regex = new Regex(Regex.Escape("0"));
                    phoneNumberFormat = regex.Replace(phoneNumber, "33", 1);
                }
                else if (phoneNumber.StartsWith("+33"))
                {
                    phoneNumberFormat = phoneNumber.Remove(0, 1);
                }
                else if (phoneNumber.StartsWith("33"))
                {
                    phoneNumberFormat = phoneNumber;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Le numéro de téléphone : " + phoneNumber + " n'est pas valide.");
                    throw new Exception("Numero de téléphone invalide ! ");
                }
            }
            else
            {
                phoneNumberFormat = null;
            }
            return phoneNumberFormat;
        }
    }
}
